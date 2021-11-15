# PackageHelper

[![Test runner](https://github.com/innerspacetrainings/PackageHelper/actions/workflows/test-runner.yml/badge.svg?branch=develop)](https://github.com/innerspacetrainings/PackageHelper/actions/workflows/test-runner.yml)

Includes version manager for package, DTValidator test and package validation tests.

# Unity Assemblies Debugger

Small utility to incrementing the version of package.

<img src=".github/example.png" width="300">

## Usage

- If you want to change the version, you can open the utility window at `Innerspace > Version Manager`.
- [DTValidator](https://github.com/innerspacetrainings/DTValidator) test are included, you can enable them in test runner by adding following line to testables.
  ```Json
  "testables": [
    "eu.innerspace.packagehelper"
  ],
  ```

## Installation

### Adding the package to the Unity project manifest

* Navigate to the `Packages` directory of your project.
* Adjust the project manifest file `manifest.json` in a text editor.
    * Ensure `https://package.openupm.com` is part of `scopedRegistries`.
        * Ensure `at.innerspace` is part of `scopes`.
    * Add `"eu.innerspace.packagehelper": "https://github.com/innerspacetrainings/PackageHelper.git?path=Assets/Package"` to `dependencies`.

  ```json
  {
    "scopedRegistries": [
      {
        "name": "package.openupm.com",
        "url": "https://package.openupm.com",
        "scopes": [
          "com.openupm",
          "at.innerspace"
        ]
      }
    ]
  }
  ```
* Switch back to the Unity software and wait for it to finish importing the added package.
