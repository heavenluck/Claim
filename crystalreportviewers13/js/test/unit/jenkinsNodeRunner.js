var qunit = require('node-qunit-phantomjs');
var fs = require('fs');

// We do not have access to the file system within the qunit module.
// Therefore, I am overloading the console.log function to write the testing result
// xml to a file.
var oldConsoleLog = {
    log: console.log
};

// Intercept "log" calls, and write to file if xml.
console.log = function(str) {
    // If there is an opening xml tag, write to file.
    if (str.substring(0,5) === "<?xml") {
        fs.writeFileSync("./results.xml", str);
    }
    else {
        oldConsoleLog.log(str);
    }
};

// Run the qunit tests
var res = qunit('./nodeTests.qunit.html');