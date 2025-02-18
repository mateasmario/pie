import os

# Define license details
LICENSE_TYPE = "GPL-3.0-or-later"  # Change this to match your license
COPYRIGHT_HOLDER = "Mario-Mihai Mateas"  # Replace with your name or organization
YEAR = "2023-2025"  # Update as needed

# Define the root directory
ROOT_DIR = "Resources"

def add_license_to_pngs(directory):
    """Generates .license files for all PNG files in the given directory."""
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(".png"):
                png_path = os.path.join(root, file)
                license_path = png_path + ".license"

                # Check if the .license file already exists
                if os.path.exists(license_path):
                    print(f"Skipping {license_path}, already exists.")
                    continue

                # Write license information
                with open(license_path, "w", encoding="utf-8") as license_file:
                    license_file.write(f"SPDX-FileCopyrightText: {YEAR} {COPYRIGHT_HOLDER}\n")
                    license_file.write(f"SPDX-License-Identifier: {LICENSE_TYPE}\n")

                print(f"Added license for {png_path}")

if __name__ == "__main__":
    add_license_to_pngs(ROOT_DIR)
    print("\n✅ Done! Run `reuse lint` to check compliance.")
