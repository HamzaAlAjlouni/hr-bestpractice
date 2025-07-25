/*
    Usage: $ phantomjs --remote-debugger-port=9001 --remote-debugger-autorun=yes debug.js page.html

    Open Chrome tab to http://localhost:9001/; open second link (ie, path to page.html)
*/
var system  = require('system' ), fs = require('fs'), webpage = require('webpage');

(function(phantom){
    var page=webpage.create();

    function debugPage(){
        console.log("Refresh a second debugger-port page and open a second webkit inspector for the target page.");
        console.log("Letting this page continue will then trigger a break in the target page.");
        page.open(system.args[1]);
        page.evaluateAsync(function() {
        });
    }
    debugPage();
}(phantom));
