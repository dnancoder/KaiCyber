# Guide to Execute the C# Web API

## Prerequisites

- Install [VS Code](https://code.visualstudio.com/).
- Clone the GitHub repository to your local machine.
- Install the following extensions in VS Code:
  1. [ .NET Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
  2. [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
  3. [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
  4. [Rest Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

## Steps to Execute the Web API

1. **Navigate to the Repository Path**
   - Git repo base path: `[Base Path]\KaiCyberScanner\KaiCyberScanner`

2. **Open Terminal in VS Code**
   - Open VS Code.
   - Use the menu bar at the top to navigate to **View** > **Terminal**.
   - The **Terminal** tab will appear at the bottom.

3. **Change Directory**
   - Use the following command to change the directory to `KaiCyberScanner\KaiCyberScanner`:
     ```sh
     cd C:\Git\KaiCyber\KaiCyberScanner\KaiCyberScanner
     ```

4. **Build and Run the Web API**
   - Run the following commands in the terminal:
     ```sh
     dotnet build
     dotnet run
     ```

5. **Verify Successful Execution**
   - After the .NET service runs successfully, it will show a success log in the terminal.

6. **Send HTTP Requests**
   - Open the file `KaiCyberScanner.http` in VS Code.
   - You can now send requests by clicking the appropriate buttons.
   - Refer to the image file `.\Documentation\HttpRequest-Execute-Guide.jpg` to see where to click to initiate Scan and Query requests.
   - The results will be displayed in the right pane of VS Code.
