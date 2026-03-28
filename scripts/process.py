import sys
import os
import time

def process_audio(input_file):
    print(f"Processing audio file: {input_file}")
    
    # Simulate processing time
    time.sleep(2)
    
    # Create base output directory
    output_base = "output"
    dirs = ["transcripts", "summaries", "quizzes"]
    
    for d in dirs:
        os.makedirs(os.path.join(output_base, d), exist_ok=True)
    
    file_name = os.path.basename(input_file)
    base_name = os.path.splitext(file_name)[0]
    
    # Mock result files
    outputs = {
        "transcripts/transcript.txt": f"Mock transcript for {file_name}.\nThis is a simulation of the speech-to-text process.",
        "summaries/summary.md": f"# Summary of {file_name}\n\n- Point 1\n- Point 2\n- Point 3",
        "quizzes/quiz.md": f"## Quiz for {file_name}\n\n1. What is the subject?\n   - A) Math\n   - B) Science\n   - C) Other"
    }
    
    for path, content in outputs.items():
        full_path = os.path.join(output_base, path)
        with open(full_path, "w", encoding="utf-8") as f:
            f.write(content)
        print(f"Generated: {full_path}")

    print("Processing complete.")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python process.py <input_file_path>")
        sys.exit(1)
        
    input_path = sys.argv[1]
    process_audio(input_path)
