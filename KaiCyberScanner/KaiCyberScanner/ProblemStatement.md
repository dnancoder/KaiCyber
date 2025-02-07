Problem Statement
Build a single Go service with two REST APIs to:
1.	Scan the GitHub repository ( https://github.com/velancio/vulnerability_scans ) for JSON files and store their contents.
2.	Query stored JSON payloads using key-value filters.
API Requirements
1. Scan API
      Endpoint: POST /scan
{
   "repo": <repo root>,
   "files": [<filename1>, <filename2>,…]
}
•	Fetches all .json files from the specified GitHub path.
•	Processes files containing arrays of JSON payloads (see example below).
•	Stores each payload with metadata (source file, scan time). Feel free to design whatever schemas and data structures are needed
2. Query API
      Endpoint: POST /query
{
    "filters": {
           "severity": "HIGH"
     }
}
•	Returns all payloads matching any one filter keys (exact matches). Focus on just one filter for the assignment “severity”.

Example Response Expected:
[
    {
              "id": "CVE-2024-1234",
              "severity": "HIGH",
              "cvss": 8.5,
              "status": "fixed",
              "package_name": "openssl",
              "current_version": "1.1.1t-r0",
              "fixed_version": "1.1.1u-r0",
              "description": "Buffer overflow vulnerability in OpenSSL",
              "published_date": "2024-01-15T00:00:00Z",
              "link": "<https://nvd.nist.gov/vuln/detail/CVE-2024-1234>",
              "risk_factors": [
                "Remote Code Execution",
                "High CVSS Score",
                "Public Exploit Available"
              ]
     },
     {
              "id": "CVE-2024-8902",
              "severity": "HIGH",
              "cvss": 8.2,
              "status": "fixed",
              "package_name": "openldap",
              "current_version": "2.4.57",
              "fixed_version": "2.4.58",
              "description": "Authentication bypass vulnerability in OpenLDAP",
              "published_date": "2024-01-21T00:00:00Z",
              "link": "<https://nvd.nist.gov/vuln/detail/CVE-2024-8902>",
              "risk_factors": [
                "Authentication Bypass",
                "High CVSS Score"
              ]
     },
     .
     .
     .  
 ]  
   
Technical Requirements
•	Language: Go (preferred).
•	Database: Any you are comfortable. sqlite (preferred)
•	Concurrency: Process ≥ 3 files in parallel.
•	Error Handling: Retry failed GitHub API calls (2 attempts).
•	Docker: Single container for the service.
Deliverables
1.	Code: 
o	Modular, production-ready Go code.
o	Unit tests (60%+ coverage for core logic).
2.	Docker Setup: 
o	Single Docker container with dependencies.
3.	Documentation: 
o	A README file should be provided, briefly documenting what you are delivering. We expect testing instructions: whether it’s an automated test framework or simple manual steps.
o	Please upload the code to a publicly accessible GitHub, GitLab or other public code repository account.
Evaluation Focus
Code Quality: Focus on readable, maintainable, extensible, modular code along with error handling and use of concurrency patterns.
Documentation: Clear instructions for running and testing.
Time & Submission
•	Time Budget: 4–5 hours (prioritize core functionality).
•	Submit Within: 5 business days.
•	Reply to this email with your repo link.
What’s NOT Evaluated:
•	Perfect feature completeness. Focus on providing just the required endpoints.
•	Advanced infrastructure (Kubernetes, etc.).
•	UI/UX design.
