DotNetNuke-QsContent
====================

Allows you to show/hide content based on querystring values.


### How it works
* Add module to page
* Go to configuration
* Enter a key/value pair of a querystring. These can be comma delimited and may contain regular expressions.
* Select the module you want the querystring values to effect.
* Select if the default is to hide the module or show the module based on the querystring key/value match. Otherwise module will be hidden if querystring does not match.
* Go to page in VIEW mode or log out and use your querystring combination to see a module disappear or appear.

**Note:** The client side check to hide content is done by using the .DnnModule-X class. So be aware that the toggling of content is merely for cosmetic reasons and not for security. If you need security use the Server side check.

**Note:** Querystring values are processed server side so you don't have to worry if url is formatted as ?test=1 OR /test/1/default.aspx

### Installation
* Go to the [install directory](https://github.com/InspectorIT/DotNetNuke-QsContent/tree/master/install) and grab the latest version.
* Install like any other DNN Module.

### Fun Facts
* Administration uses [AngularJS](http://angularjs.org/) in order to wire up client UI.
* Web Service handles user validation and module permissions. 
    * Supporting DotNetNuke 6.x so no WebAPI at this time.
* `ControlUtilities.FindFirstDescendent<Control>()` to find another module in the stack.

### Roadmap
- <del>Allow for server side hide/show of content</del> Done 5/19/2013.
- <del>Allow users to edit items. (just haven't gotten around to it yet</del> Done 5/19/2013
- <del>Make admin pretty.</del> Done 5/19/2013
- Show stats when logged in.
- Add date time stamps to enable/expire a check.

### Requirements
* DotNetNuke Version 6.2.0

### Screenshot

![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/Qscontent-v2.png)
