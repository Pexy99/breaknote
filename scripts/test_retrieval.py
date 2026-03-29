import os
import shutil
import datetime
from retrieval import find_candidates, score_folder

def test_retrieval_logic():
    test_dir = "tmp_test_materials"
    if os.path.exists(test_dir):
        shutil.rmtree(test_dir)
    os.makedirs(test_dir)

    # Folders based on the user's provided structure
    mar_root = os.path.join(test_dir, "3월 강의자료")
    f1 = os.path.join(mar_root, "0318-0319 강의자료(김승준)")
    f2 = os.path.join(mar_root, "0320-0324")
    f3 = os.path.join(mar_root, "0325-0327 강의자료(김충열)")

    os.makedirs(f1)
    os.makedirs(f2)
    os.makedirs(f3)

    # Files
    open(os.path.join(f1, "Cloud Computing 1.pdf"), 'w').close()
    open(os.path.join(f2, "Azure Blob Storage.pdf"), 'w').close()
    open(os.path.join(f3, "Pandas.pdf"), 'w').close()

    # Test Case 1: March 19th audio
    audio_date = datetime.date(2026, 3, 19)
    print(f"Testing for date: {audio_date}")
    
    candidates = find_candidates(test_dir, audio_date)
    
    print(f"Found {len(candidates)} candidates.")
    for c in sorted(candidates, key=lambda x: x['folder_score'], reverse=True):
        print(f" - {c['filename']} in {os.path.basename(os.path.dirname(c['path']))}: {c['folder_score']}")

    # Verification
    top_candidate = max(candidates, key=lambda x: x['folder_score'])
    if "Cloud Computing 1.pdf" in top_candidate['filename'] and top_candidate['folder_score'] == 1.0:
        print("✅ Phase A Validation PASSED: Correct folder prioritized.")
    else:
        print("❌ Phase A Validation FAILED: Proximity scoring incorrect.")

    # Cleanup
    shutil.rmtree(test_dir)

if __name__ == "__main__":
    test_retrieval_logic()
