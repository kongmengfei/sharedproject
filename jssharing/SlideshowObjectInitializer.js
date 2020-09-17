function SlideshowObjectInitializer() {

    ShowPic = (function (ShowPicOrig) {
        return function () {

            var ssObj = arguments[0];  //SlideShow object

            console.log(ssObj);

            ShowPicOrig.apply(this, arguments); //call original ShowPic

            if (ssObj.image.currentSrc == 'http://sp2016/ImagesPicLib/_w/surf4_jpg.jpg') {
                ssObj.link.href = 'https://www.google.com';  //<--put your custom picture link url here 
            }
        };
    })(ShowPic);

}

ExecuteOrDelayUntilScriptLoaded(SlideshowObjectInitializer, 'imglib.js');
