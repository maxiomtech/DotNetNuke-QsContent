DotNetNuke-QsContent
====================

Allows you to show/hide content based on querystring values.


### How it works
* Add module to page
* Go to configuration
* Enter a key/value pair of a querystring. These can be comma delimited and may contain regular expressions.
* Select the module you want the querystring values to effect.
* Select if the default is to hide the module or show the module based on the querystring key/value match.

The content is hidden by using the .DnnModule-X class. So be aware that the toggling of content is merely for cosmetic reasons and not for security.

### Roadmap
* Allow for server side hide/show of content.
* Allow users to edit items. (just haven't gotten around to it yet)
* Make admin pretty.
* Show stats when logged in.

### Requirements
* DotNetNuke Version 6.2.0

### Screenshot

![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/DotNetNuke-Qscontent.png)