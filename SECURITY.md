# Security Policy

## Supported version
The latest `main` branch is supported.

## Reporting
Please report suspected vulnerabilities privately through GitHub's security reporting features when available rather than publishing exploit details in a public issue. Include affected version, reproduction steps, impact, and a minimal test case without personal data.

## Security model
Desktop Search is local-first and makes no network requests. It reads filesystem metadata and, only with `--content`, eligible file contents. It does not sandbox files, elevate privileges, or promise forensic handling of hostile files. Run it with least privilege and avoid searching untrusted network mounts when that risk is unacceptable.

Maintainer: Radwan Abdulhadi Ahmed (@rad03i2).