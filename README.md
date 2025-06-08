# FileIndexingSolution

## Overview
This project is a distributed file indexing system implemented in C# using .NET. It consists of a Master application and two Agents (AgentA and AgentB).  
The Agents scan directories for text files, count word occurrences, and send the data to the Master, which combines and displays the results.

## Features
- Scans files and counts word frequency.
- Communicates between Master and Agents using TCP sockets.
- Supports multiple Agents sending data concurrently.
- Displays combined word counts from all agents.
- Includes a UML diagram for system design.
- Tested on macOS (with some features limited to Windows).

## How to Run
1. Run the Master application first (listens on ports 5001 and 5002).  
2. Run AgentA with the path to the directory to scan:  
   `dotnet run -- "path/to/AgentA/files"`  
3. Run AgentB with the path to the directory to scan:  
   `dotnet run -- "path/to/AgentB/files"`  
4. The Master will receive and combine the data from both agents and print the results.

## Known Issues / Limitations
- Setting processor affinity is implemented but does **not** work on macOS. This feature is only supported on Windows.
- Some minor platform-specific behavior might occur when running on non-Windows OS.

## Project Structure
- `Master/` - Master application code  
- `AgentA/` - AgentA application code  
- `AgentB/` - AgentB application code  
- `README.md` - This file  
- `UML_Diagram.png` - UML diagram image (optional)

## Contact
Developed by Mayank  
Email: mayank@example.com (replace with your email if you want)  

---

Feel free to modify this README with your screenshots and UML diagram links after uploading.

If you want, I can also generate a README.md file for you ready to push to GitHub!
