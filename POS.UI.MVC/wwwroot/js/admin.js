if (typeof jQuery === "undefined") {
    throw new Error("jQuery plugins need to be before this file");
}

var admin = {
    loadTimeLog: function (loadTime, loadingInfo) {
        var d = Date.diff(admin.getGMTTime(), loadTime);    //using DateDiff.js
        var min = d.minutes; if (min < 10) {
            min = "0" + min;
        }
        var sec = d.seconds; if (sec < 10) {
            sec = "0" + sec;
        }

        console.log("[" + min + ":" + sec + "] " + ">> " + loadingInfo);
    },

    getGMTTime: function () {
        var time = new Date();
        var gmtMS = time.getTime() + (time.getTimezoneOffset() * 60000);
        var gmtTime = new Date(gmtMS);
        return gmtTime;
    }
}




