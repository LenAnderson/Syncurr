# Syncurr
Application to sync local directories with Imgur albums

Directories and Imgur albums added to Syncurr will be checked for changes every 10 minutes and synced.

## Warning
**Albums and images removed from Imgur will also be deleted locally.**

**Albums and images you own will also be removed from Imgur when you delete the local copy!**


## Installation
Download and unzip the [latest release](https://github.com/LenAnderson/Syncurr/releases/latest). Launch Syncurr.exe to start Syncurr.


## Instructions

![screenshot](https://i.imgur.com/DMqke93.png)

### Imgur Authentication
The first time Syncurr starts to synchronize it will ask for a pin from Imgur by opening the Imgur in your browser.
Enter the pin into the text field in Syncurr and confirm to let Syncurr retrieve the access tokens from Imgur.

### Me
If "Sync My Account" is activated in the settings Syncurr will automatically download all your albums into your default account directory. 

Folders added to the local account directory will be uploaded to Imgur as new albums.

Folders removed from the local account directory will be deleted from Imgur.

Albums created on Imgur will be added to the local directory as new folders.

Albums deleted on Imgur will also be deleted locally.


### Users
Drag one or multiple folders from Windows Explorer onto the Syncurr window and choose to "Add User" to sync that folder with an Imgur user.

Alternatively you can drag the URL of a user page from your browser onto the Syncurr window to add that user to the list of synchronized users.

Only the account you used to provide a pin for Syncurr can upload and delete images or albums from Imgur. You cannot modify albums from other users.

### Albums
Drag one or multiple folders from Windows Explorer onto the Syncurr window and choose to "Add Album" to sync that folder with an Imgur album.

Alternatively you can drag the URL of an album from your browser onto the Syncurr window to add that album to the list of synchronized albums.

Only albums owned by the account you used to provide a pin for Syncurr can upload and delete images from Imgur. You cannot modify albums from other users.
