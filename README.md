# Syncurr
Application to sync local directories with Imgur albums

Directories and Imgur albums added to Syncurr will be checked for changes every 10 minutes and synced.


## Installation
Download and unzip the [latest release](https://github.com/LenAnderson/Syncurr/releases/latest). Launch Syncurr.exe to start Syncurr.


## Instructions

### Proxy
If you need to use a proxy the first thing you should do is configure the proxy settings in the Proxy tab.
Choose between HTTP and SOCKS proxy and enter the proxy's URL (**for SOCKS proxies the proxy URL needs to be an IP address**), port, and if required the credentials.

The internal proxy URL is only used with SOCKS proxy and is just needed for Syncurr to route the proxy connections. The URL is usually just <code>127.0.0.1</code>. Choose a free port for the internal routing.

### Imgur Authentication
Once the proxy configuration is completed you need to log in to Imgur and authorize Syncurr to use your account.

In the Imgur Authorization tab click on the button labeled "Get Pin". This will open Imgur in your browser and provide a pin for Syncurr. Enter the pin into the text field and click on the "Get Tokens" button to retrieve the access tokens from Imgur.

You can now start using Syncurr!

### Albums
To start syncing folders simply drag one or multiple folder from Windows Explorer onto the Syncurr window. All added folders will show up in the Albums tab.
Double click (or choose Properties... from the context menu) an entry to set up the Album ID or change the path.


## Acknowledgements
Using SOCKS proxies is made possible thanks to Landon Key's SocksWebProxy project which can be found here:
https://github.com/postworthy/SocksWebProxy
