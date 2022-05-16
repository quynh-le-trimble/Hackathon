# Hackathon

#### Platform:  
#### Concept:

##### Perspective: 


## Setting up your local development

### Install Unity 
 - Version 2021.3.1f1

### Visual Studio Code
- Install the following extensions:
  -  Debugger for Unity (Unity)
  -  Unity Code Snippets (Kleber Silva)


## Folder Structure

#### _Scripts

- Managers, ie GameManager
- Scriptables
- Systems, ie AudioSystem
- Utilies: All utiliy and helper classes.

#### Art
Contains all art assets such as textures, materials, visual effects, shaders, models, etc.

#### Audio

- Music
- Sounds

#### Plugins
Contains all external and third party assets.

**DO** move all assets into this folder after importing. 

#### Prefabs
Contains all prefabs. 

#### Resources
Contains all data objects, ie scriptable objects. 

#### Scenes
Contains all scenes. 
- Primary entry scene name: `Main`

#### Settings
URP Settings. Pre-generated from template. 



### Networking Workflow

Use this workflow while testing networking:
1. Clone repo (this is your master).
2. Create new folder and suffix/prefix with SYNC. (Any name will work)
3. Open up Powershell(IDE) and add the following script:

```
$srcDir = "C:\<<FULL PATH>>\GitHub\Hackathon"
$excludeDir = ($srcDir + "\Library")
$destDir = "C:\<<FULL PATH>>\GitHub\HackathonSYNC"

# Options
# /FFT - Assumes FAT file times (2-second precision)
# /z Restartable Mode
# /xa:h - Exclude hidden files
# /w:5 - wait 5 seconds between retries. 


Robocopy $srcDir $destDir /MIR /FFT /Z /XA:H /W:5 /XD $excludeDir

```
4. The first time will copy all files and might take some time. 
5. Open up Unity using the `master` folder, like normal. 
6. Add a new project, in Unity Hub and point it to the SYNC folder. 
7. Open the project and work only from the `master` project. 
8. When changes are made: Run the script above to sync any changes. Note: this will also delete files. 
9. If the script is successful, Enter Play Mode with both instances of Unity. 

