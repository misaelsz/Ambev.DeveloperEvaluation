# Get Start

1. Install WSL using the official microsoft documentation:
https://learn.microsoft.com/en-us/windows/wsl/install

2. In the powershell run wsl distro using: wsl -d [your distro name here]

   example: wsl -d ubuntu 

3. To install docker in the linux distro use the official Docker documentation:
https://docs.docker.com/engine/install/ubuntu/

4. Export data folder path using:
export APPDATA="/mnt/c/Users/$(cmd.exe /c "echo %USERNAME%" | tr -d '\r')/AppData/Roaming"


5. Run docker compose: docker compose -f "{replace with the file path}docker-compose.yml" up -d
