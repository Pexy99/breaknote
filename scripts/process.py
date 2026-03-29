import sys
import os
import datetime
import whisper
import logging
from retrieval import retrieve_materials

# Configure logging
LOG_DIR = "logs"
os.makedirs(LOG_DIR, exist_ok=True)
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s [%(levelname)s] %(message)s',
    handlers=[
        logging.FileHandler(os.path.join(LOG_DIR, "process.log"), encoding='utf-8'),
        logging.StreamHandler(sys.stdout)
    ]
)
logger = logging.getLogger("breaknote")

def setup_ffmpeg_path():
    """Ensure ffmpeg is in the PATH for Whisper."""
    # Common WinGet installation path for Gyan.FFmpeg
    local_app_data = os.environ.get("LOCALAPPDATA", "")
    if local_app_data:
        winget_ffmpeg_base = os.path.join(local_app_data, "Microsoft", "WinGet", "Packages")
        # Try to find the specific Gyan.FFmpeg folder
        if os.path.exists(winget_ffmpeg_base):
            for folder in os.listdir(winget_ffmpeg_base):
                if "Gyan.FFmpeg" in folder:
                    # Look for bin folder inside the ffmpeg full build
                    bin_path = None
                    full_folder_path = os.path.join(winget_ffmpeg_base, folder)
                    for sub in os.listdir(full_folder_path):
                        if "ffmpeg" in sub and os.path.isdir(os.path.join(full_folder_path, sub)):
                            potential_bin = os.path.join(full_folder_path, sub, "bin")
                            if os.path.exists(potential_bin):
                                bin_path = potential_bin
                                break
                    
                    if bin_path and bin_path not in os.environ["PATH"]:
                        os.environ["PATH"] += os.pathsep + bin_path
                        logger.info(f"Added FFmpeg to PATH: {bin_path}")
                        break

def process_audio(input_file, sync_folder=""):
    # Ensure ffmpeg is visible
    setup_ffmpeg_path()
    
    logger.info(f"Loading Whisper model (base)...")
    # Load model (base: fast for iteration and local testing)
    model = whisper.load_model("base")
    
    logger.info(f"Transcribing audio file: {input_file} (Language: Korean)")
    # Run transcription
    # Specifying language="ko" and other parameters improves accuracy for Korean audio
    # - fp16=False: avoids warnings and inaccuracy on CPU
    # - condition_on_previous_text=False: prevents endless loop/hallucinations
    # - initial_prompt: provides context to generate natural sentences
    result = model.transcribe(
        input_file, 
        language="ko",
        fp16=False,
        condition_on_previous_text=False,
        verbose=True,
        initial_prompt="다음은 한국어로 진행된 음성 녹음입니다. 자연스럽고 정확한 한국어 문장으로 기록합니다."
    )
    actual_transcript = result["text"].strip()
    
    attached_text = ""
    if sync_folder and os.path.exists(sync_folder):
        logger.info("Starting Auto-Selection of Lecture Materials...")
        file_stat = os.stat(input_file)
        audio_date_str = datetime.datetime.fromtimestamp(file_stat.st_mtime).strftime('%Y-%m-%d')
        
        materials = retrieve_materials(sync_folder, audio_date_str, actual_transcript)
        for m in materials:
            attached_text += f"\n--- Source: {m['filename']} ---\n{m['text'][:500]}...\n"
    else:
        logger.warning("No valid sync folder provided. Skipping material selection.")
        
    # Create base output directory
    output_base = "output"
    dirs = ["transcripts", "summaries", "quizzes"]
    
    for d in dirs:
        os.makedirs(os.path.join(output_base, d), exist_ok=True)
    
    file_name = os.path.basename(input_file)
    base_name = os.path.splitext(file_name)[0]
    
    # Real transcript + Mock summary/quiz
    summary_mock = f"# Summary of {file_name}\n\n- (Mock) Point 1 from AI logic\n- (Mock) Point 2 from AI logic\n- (Mock) Point 3 from AI logic\n"
    quiz_mock = f"## Quiz for {file_name}\n\n1. (Mock) What is the subject?\n   - A) Math\n   - B) Science\n   - C) Other\n"
    
    if attached_text:
        summary_mock += f"\n\n## (Mock) Attached Context Used:\n{attached_text}"
        quiz_mock += f"\n\n## (Mock) Attached Context Used:\n{attached_text}"

    outputs = {
        "transcripts/transcript.txt": actual_transcript,
        "summaries/summary.md": summary_mock,
        "quizzes/quiz.md": quiz_mock
    }
    
    for path, content in outputs.items():
        full_path = os.path.join(output_base, path)
        with open(full_path, "w", encoding="utf-8") as f:
            f.write(content)
        logger.info(f"Generated: {full_path}")
    
    logger.info("Processing complete.")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        logger.error("Usage: python process.py <input_file_path> [sync_folder]")
        sys.exit(1)
        
    input_path = sys.argv[1]
    sync_folder = sys.argv[2] if len(sys.argv) > 2 else ""
    
    # Simple check for file existence
    if not os.path.exists(input_path):
        logger.error(f"Error: File not found at {input_path}")
        sys.exit(1)

    try:
        process_audio(input_path, sync_folder)
    except Exception as e:
        logger.exception(f"Unexpected error: {str(e)}")
        sys.exit(1)
