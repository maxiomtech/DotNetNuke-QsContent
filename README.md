DotNetNuke-QsContent
====================

Allows you to show/hide content based on querystring values.


### How it works
* Add module to page
* Go to configuration
* Enter a key/value pair of a querystring. These can be comma delimited and may contain regular expressions.
* Select the module you want the querystring values to effect.
* Select if the default is to hide the module or show the module based on the querystring key/value match. Otherwise module will be hidden if querystring does not match.

The content is hidden by using the .DnnModule-X class. So be aware that the toggling of content is merely for cosmetic reasons and not for security.

### Roadmap
* <del>Allow for server side hide/show of content</del> Done 5/19/2013.
* <del>Allow users to edit items. (just haven't gotten around to it yet</del> DOne 5/19/2013
* <del>Make admin pretty.</del> Done 5/19/2013
* Show stats when logged in.

### Requirements
* DotNetNuke Version 6.2.0

### Screenshot

![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/Qscontent-v2.png)