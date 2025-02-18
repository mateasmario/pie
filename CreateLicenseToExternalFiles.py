# SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com>
# SPDX-License-Identifier: GPL-3.0-or-later

import os

SPDX_FileCopyrightText = "SPDX-FileCopyrightText"
SPDX_LicenseIdentifier = "SPDX-License-Identifier"

LICENSE_TYPE = "GPL-3.0-or-later"  # Change this to match your license
COPYRIGHT_HOLDER = "Mario-Mihai Mateas"  # Replace with your name or organization
YEAR = "2023-2025"  # Update as needed

DIRS = [
    "ROOT",
    "Resources", 
    "Icons",
    "Properties",
    "Forms",
    "Forms\\BuildCommands",
    "Forms\\CodeTemplates",
    "Forms\\Databases",
    "Forms\\Format",
    "Forms\\Git",
    "Forms\\MainForm",
    "Forms\\Notifications",
    "Forms\\Other",
    "Forms\\Theme"
]

def add_license_to_pngs(directory):
    for root, _, files in os.walk(directory):
        for file in files:
            if not file.endswith(".license"):
            	png_path = os.path.join(root, file)
            	license_path = png_path + ".license"

            	# Check if the .license file already exists
            	if os.path.exists(license_path):
                	print(f"Skipping {license_path}, already exists.")
                	continue

            	# Write license information
            	with open(license_path, "w", encoding="utf-8") as license_file:
                	license_file.write(f"{SPDX_FileCopyrightText}: {YEAR} {COPYRIGHT_HOLDER}\n")
                	license_file.write(f"{SPDX_LicenseIdentifier}: {LICENSE_TYPE}\n")

            	print(f"Added license for {png_path}")

if __name__ == "__main__":
    for dir in DIRS:
        add_license_to_pngs(dir)
    print("\n✅ Done! Run `reuse lint` to check compliance.")
