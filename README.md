# Windows Service Project README

This repository contains a Windows Service project that runs a batch file when the PC starts up and logs the service's status to the `service_log.txt` file under the `logs` directory located same path with `CST_Service.exe` file.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)

## Introduction

The purpose of this project is to create a Windows Service that executes a batch file during system startup. The service then logs its status (whether it's working or not) to a log file for monitoring purposes.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- Windows operating system (compatible with the version of Windows you are using)
- Administrative privileges to install and manage services

## Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/red1xe/CST_Service.git
   ```

2. Create a `system variables`:

   - Search the `Edit the system environment variable` on the search bar.
   - Open the `Environment Variables`.
   - Create a `new` system variable on the second part.
   - Variable name: `CST_Service_PATH`.
   - Variable value: `Path_your_batch_file`. e.g: `C:\Users\main.bat`.
   - Press OK.

3. Give acces to open batch file via windows services.

   - `Win + R` > `control admintools` > `Local Security Policy` > `Local Policies` > `User Rights Assignment` > `Deny log on as a service` > `Add User or Group` > Enter the name of the user that you want to grant the right to log on as a service > `Check Names` > `OK` > `Apply` > `OK`

3. Create a service on your local machine with `InstallUtil.exe`:

   - Open your Command Prompt with administrator.
   - ```bash
     cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
     ```
   - ```bash
     C:\Windows\Microsoft.NET\Framework\v4.0.30319>
     ```
   - In this point we have to get path of `CST_Service.exe` e.g if we clone the repo at `C:\` _`C:\CST_Service\bin\Debug\CST_Service.exe`_

   - ```bash
     C:\Windows\Microsoft.NET\Framework\v4.0.30319>InstallUtil.exe C:\CST_Service\bin\Debug\CST_Service.exe
     ```
   - Your service installation is done!

## Configuration

- `Win + R` > `services.msc` > find the `TC_CST_Services_Startup` > Right Click > Properties
- If you set the startup type: `Automatic`, the service will run automatically when your computer on.
