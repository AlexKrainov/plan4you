var v = null;
$(function () {
    action.init().then(function () {
        //Рендерим vue.js компоненты
        v = new Vue({
            el: ".vue-action-components"
        });

        //Добавляем карусель после vue.js
        testimonials_carousel = $(".testimonials-carousel").owlCarousel({
            //autoplay: true,
            dots: true,
            loop: true,
            //nav: true,
            autoplayHoverPause: true,
            responsive: { 0: { items: 1 }, 768: { items: 2 }, 900: { items: 3 } }
        });
    });
});

var action = {
    action_obj: {
        check_sheets: {}
    },
    init: function () {
        return action.refresh_action_from_server().then(function (result) {
            action.action_obj = result
        });
    },
    onDownload: function (obj) {
        console.log(obj);
        let $span = $(this).parent().find("span[data-action=downloads]");
        let data_type = $span.attr("doctype");
        let fileID = $span.attr("data-id");

        action.refresh_count_download(data_type, fileID).then((result) => {
            if (result.is_ok == true) {

                $span.html(result.downloads);

                //обновляем данные в глобальном массиве
                if (data_type == "check_sheets") {
                    let el = action.action_obj.check_sheets.find(x => x.id == fileID);
                    el.downloads = result.downloads;
                }
            }
        });
    },
    refresh_action_from_server: function () {
        return $.ajax({
            url: '/Action/Get',
            type: "GET",
            success: function (result) {
                return result;
            }
        });
    },
    refresh_count_download: function (data_type, fileID) {
        return $.ajax({
            url: '/Action/GetCountDownload',
            type: "POST",
            data: { datatype: data_type, id: fileID },
            success: function (result) {
                return result;
            }
        });
    }

};

//Example view download
    //<span>
    //       <span data-type="check_sheets" data-action="likes" data-id="38b4c5ed-bd2a-e811-a3e8-18dbf2192184">0</span>
    //       <a href="" onclick="return false;" class="not_selected"><i class="fa fa-heart"></i></a>
    //</span>
/*
{
  "actoin": {
    "check_sheets": {
      "id": 1,
      "is_like": false,
      "likes": 34,
      "downloads": 34
    }
  }
}

*/