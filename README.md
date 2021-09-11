```
  _     _                     ____  _  _      ____                      _      
 | |   (_)_ __  _   ___  __  / ___|| || |_   / ___|___  _ __  ___  ___ | | ___ 
 | |   | | '_ \| | | \ \/ / | |  |_  ..  _| | |   / _ \| '_ \/ __|/ _ \| |/ _ \
 | |___| | | | | |_| |>  <  | |__|_      _| | |__| (_) | | | \__ \ (_) | |  __/
 |_____|_|_| |_|\__,_/_/\_\  \____||_||_|    \____\___/|_| |_|___/\___/|_|\___|
                                                                               
```

The powerful C# analysis console in Linux

**Feature**
- Can select C# compiler version.
- Uses Pre-import system that can define "using" code globally.
- Support latest verions.
- Configurations saves as "config.cf" file. you can change configurations with custom editor.
- Command prefix is [ \` ]

**How to install runtime**
- In Ubuntu (21.04) - Refer to Microsoft's official documentation

Installing with APT can be done with a few commands. Before you install .NET, run the following commands to add the Microsoft package signing key to your list of trusted keys and add the package repository.

Open a terminal and run the following commands:

```
wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```

Now, install runtime with apt.

```
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-runtime-5.0
```

- [All of Linux](https://docs.microsoft.com/en-us/dotnet/core/install/linux)

**cf.) Install runtime not a sdk. And you must install net 5.0 runtime.**

**How to run**
- If you didn't install runtime, See "How to install runtime".
- Download release zip file, unzip it and run "./Linux CS Console" in command line

**Contact**
- Discord : 단비#1004
- Gmail : Luatronq@gmail.com

**Information**
- Release configuration : Optimize=true, OutputPath=bin\Release
- .NET version : NET 5.0
- Used libraries : [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json), [Roslyn:CodeAnalysis](https://github.com/dotnet/roslyn)

**Note**
- This app is compiled and built in Ubuntu (21.04). Therefore, compatibility with other platform is not guaranteed.
- I check this app in windows. And it works nice. If you wanna use this in windows, download source and build it. You can build as "Release" configuration.
