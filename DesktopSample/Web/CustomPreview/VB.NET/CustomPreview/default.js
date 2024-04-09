
function bodyResize() {
    resizeBan();
}
function resizeBan() {


    if (msieversion() > 4) {
        try {


            if (document.body.clientWidth == 0) return;


            var oBanner = document.all.item("pagetop");

            var oText = document.all.item("pagebody");
            if (oText == null) return;
            var oBannerrow1 = document.all.item("projectnamebanner");
            var oTitleRow = document.all.item("pagetitlebanner");
            if (oBannerrow1 != null) {
                var iScrollWidth = dxBody.scrollWidth;
                oBannerrow1.style.marginRight = 0 - iScrollWidth;
            }
            if (oTitleRow != null) {
                oTitleRow.style.padding = "0px 10px 0px 22px; ";
            }
            if (oBanner != null) {
                document.body.scroll = "no"
                oText.style.overflow = "auto";
                oBanner.style.width = document.body.offsetWidth - 2;
                oText.style.paddingRight = "20px"; // 幅の問題コード
                oText.style.width = document.body.offsetWidth - 4;
                oText.style.top = 0;
                if (document.body.offsetHeight > oBanner.offsetHeight)
                    oText.style.height = document.body.offsetHeight - (oBanner.offsetHeight + 4)
                else oText.style.height = 0
            }
            try { nstext.setActive(); } //キーボードからスクロールするとすぐにページがロードされることができます。唯一のIE 5.5以上で動作します。
            catch (e) { }

        }
        catch (e) { }
    }
}

function msieversion()
// 他人のためには、Microsoft Internet Explorer（メジャー）バージョン番号、または0を返す。
// この関数は"MSIE"という文字列を検索し、バージョン番号を抽出することで動作
// アップは無視され、マイナーバージョン、の小数点に、スペースに続く。
{
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)        // Microsoft Internet Explorerがある。リターンのバージョン番号
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
    else
        return 0    // 他のブラウザです。
}


function bodyLoad() {

    resizeBan();
    document.body.onresize = bodyResize;
}
