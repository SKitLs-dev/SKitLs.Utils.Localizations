# SKitLs.Utils.Localizations ![GitHub](https://img.shields.io/github/license/Sargeras02/SKitLs.Utils.Localizations) ![Nuget](https://img.shields.io/nuget/v/SKitLs.Utils.Localizations) [![CodeFactor](https://www.codefactor.io/repository/github/sargeras02/skitls.utils.localizations/badge)](https://www.codefactor.io/repository/github/sargeras02/skitls.utils.localizations) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/Sargeras02/SKitLs.Utils.Localizations)

Comprehensive localization framework that facilitates seamless string translation and adaptation for diverse language contexts.

## Description

The project consists of three essential elements, each contributing to a robust and efficient localization mechanism.

1. `enum LangKey`:

    The LangKey enumeration comprises language keys essential for localization purposes.
    It provides a standardized set of language identifiers, enabling clear categorization
    and streamlined handling of localized content for diverse language options.

2. `interface ILocalizator`
    
    The ILocalizator interface serves as a specialized mechanism for localizing strings, leveraging the provided language key.
    By adhering to this interface, developers can seamlessly integrate the localization functionality into their applications,
    allowing for enhanced user experience across various language preferences.

3. `class DefaultLocalizator`:
    
    The DefaultLocalizator class represents a specialized service that empowers efficient string localization
    based on the language key supplied.
    Serving as a default realization of the ILocalizator interface, this class offers a reliable and readily available solution
    for developers seeking to implement localization capabilities in their projects.
    It ensures the seamless integration of localized content, promoting a user-friendly experience within diverse language contexts.

By combining the elements of the ILocalizator interface, the LangKey enumeration, and the DefaultLocalizator class,
the project delivers a comprehensive localization solution to enhance the global reach and appeal of applications
while ensuring a professional and localized user experience.

## Setup

### Requirements

- Newtonsoft.Json 13.0.3 or higher

Before running the project, please ensure that you have the following dependencies installed and properly configured in your development environment.

### Installation

1. Using Terminal Command:
    
    To install the project using the terminal command, follow these steps:

    1. Open the terminal or command prompt.
    2. Run command:

    ```
    dotnet add package SKitLs.Utils.Localizations
    ```

2. Using NuGet Packages Manager:

    To install the project using the NuGet Packages Manager, perform the following steps:

    1. Open your preferred Integrated Development Environment (IDE) that supports NuGet package management (e.g., Visual Studio).
    2. Create a new project or open an existing one.
    3. Select "Project" > "Manage NuGet Packages"
    4. In the "Browse" tab, search for the project package you want to install.
    5. Click on the "Install" button to add the selected package to your project.
    5. Follow any additional setup instructions or configurations provided in the project's documentation.

3. Downloading Source Code and Direct Linking:

    To install the project by downloading the source code and directly linking it to your project, adhere to the following steps:

    1. Visit the project repository on [GitHub](https://github.com/Sargeras02/SKitLs.Utils.Localizations.git)
    2. Click on the "Code" button and select "Download ZIP" to download the project's source code as a zip archive.
    3. Extract the downloaded zip archive to the desired location on your local machine.
    4. Open your existing project or create a new one in your IDE.
    5. Add the downloaded project files to your solution using the "Add Existing Project" option in your IDE's solution explorer.
    6. Reference the project in your solution and ensure any required dependencies are resolved.
    7. Follow any additional setup or configuration instructions provided in the project's documentation.

Please note that each method may have specific requirements or configurations that need to be followed for successful installation.
Refer to the project's documentation for any additional steps or considerations.

## Usage

1. Create locals JSON

    "path/to/locals/en.name.json"
    ```JSON
    {
        "local.KeyNotDefined": "String with a key {1} is not defined in language {0} ({2}). Format params: ",
        "welcome_message": "Welcome to the project!",
        "greeting": "Welcome, {0}!",
        "farewell_message": "See you soon!"
    }
    ```

    "path/to/locals/ru.name.json"
    ```JSON
    {
        "local.KeyNotDefined": "Строка с ключом {1} не определена в языковом пакете {0} ({2}). Параметры форматирования: ",
        "welcome_message": "Добро пожаловать в проект!",
        "greeting": "Добро пожаловать, {0}!",
        "farewell_message": "До встречи!"
    }
    ```

    "path/to/locals/fr.name.json"
    ```JSON
    {
        "local.KeyNotDefined": "La chaîne avec une clé {1} n'est pas définie dans le langage {0} ({2}). Paramètres de format: ",
        "welcome_message": "Bienvenue dans le projet!",
        "greeting": "Bienvenue, {0}!",
    }
    ```

    "path/to/locals/es.name.json"
    ```JSON
    { }
    ```

2. Initialize the DefaultLocalizator:

    ```C#
    ILocalizator localizator = new DefaultLocalizator("path/to/locals"); // "resources/locals" by default
    ```

3. Resolve Localized Strings:

    ```C#
    // Example: Resolve a localized string for the English language (EN) with a specific key.
    string localizedString = localizator.ResolveString(LangKey.EN, "welcome_message");
    // -> Welcome to the project!

    // Example: Resolve a localized string with format parameters.
    string formattedString = localizator.ResolveString(LangKey.RU, "greeting", "John Doe");
    // -> Добро пожаловать, John Doe!
    ```

4. Fallback Localization:

    ```C#
    // Example: Fallback to English (EN) if the string is not defined in the specified language.
    string fallbackString = localizator.ResolveString(LangKey.FR, "farewell_message");
    // [FR.farewell_message = None] => [EN.farewell_message]
    // -> See you soon!

    // Example: Fallback to a predefined "Not Defined" string with format parameters.
    string notDefinedString = localizator.ResolveString(LangKey.ES, "invalid_input", "param1", "param2");
    // [ES.invalid_input = None] => [EN.invalid_input = None] => [EN.NotDefined]
    // -> String with a key invalid_input is not defined in language ES ("path/to/locals"). Format params: param1, param2.
    ```

By following these steps, you can seamlessly integrate the localization framework into your project, ensuring a smooth and localized user experience.

Customize the localization resource files to cater to different language options and enhance the international appeal of your application.

## Contributors

Currently, there are no contributors actively involved in this project.
However, our team is eager to welcome contributions from anyone interested in advancing the project's development.

We value every contribution and look forward to collaborating with individuals who share our vision and passion for this endeavor.
Your participation will be greatly appreciated in moving the project forward.

Thank you for considering contributing to our project.

## License

This project is distributed under the terms of the MIT License.

Copyright (C) 2023-2024, Sargeras02

## Developer contact

For any issues related to the project, please feel free to reach out to us through the project's GitHub page.
We welcome bug reports, feedback, and any other inquiries that can help us improve the project.

You can also contact the project owner directly via their GitHub profile at the [following link](https://github.com/Sargeras02) or email: skitlsdev@gmail.com

Your collaboration and support are highly appreciated, and we will do our best to address any concerns or questions promptly and professionally.
Thank you for your interest in our project.

## Notes

Thank you for choosing our solution for your needs, and we look forward to contributing to your project's success.
