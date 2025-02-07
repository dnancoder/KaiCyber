INSERT INTO Vulnerabilities (id, severity, cvss, status, package_name, current_version, fixed_version, description, published_date, link, risk_factors) VALUES (
    'CVE-2024-1111',
    'CRITICAL',
    9.9,
    'active',
    'openssl',
    '3.0.0',
    '3.0.1',
    'Critical buffer overflow in OpenSSL TLS handling',
    '2025-01-28T00:00:00Z',
    'https://nvd.nist.gov/vuln/detail/CVE-2024-1111',
    '["Buffer Overflow", "Critical CVSS Score", "Public Exploit Available", "Exploit in Wild"]'
);
