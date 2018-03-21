$(function () {
    action.init().then(function () {
        action.refresh();
        action.subscribe_to_event();
    });


});

var action = {
    action_obj: {
        check_sheets: {

        }
    },

    init: function () {
        return action.refresh_action_from_server().then(function (result) {
            action.action_obj = result
            //for (var i = 0; i < result.check_sheets.length; i++) {
            //    //action.action_obj.check_sheets[result.check_sheets[i].id] = {
            //    //    "is_like": result.check_sheets[i].is_like,
            //    //    "likes": result.check_sheets[i].likes,
            //    //    "downloads": result.check_sheets[i].downloads
            //    //};
            //    new Vue({
            //        el: 'span[data-id=' + result.check_sheets[i].id + '][data-action=likes]',
            //        data: action.action_obj.check_sheets[i]
            //    });
            //}
            //var app = new Vue({
            //    el: '#testimonials',
            //    data: action
            //});
        });
    },
    //Example view likes/download
    //<span>
    //       <span data-type="check_sheets" data-action="likes" data-id="38b4c5ed-bd2a-e811-a3e8-18dbf2192184">0</span>
    //       <a href="" onclick="return false;" class="not_selected"><i class="fa fa-heart"></i></a>
    //</span>
    refresh: function () {
        for (var i = 0; i < action.action_obj.check_sheets.length; i++) {
            //let obj = $("span[data-type=check_sheets]span[data-id=" + action.action_obj.check_sheets[i].id + "][data-action=downloads]");
            //$(obj).find("span[data-action=downloads]").text(action.action_obj.check_sheets[i].downloads);
            //$(obj).find("span[data-action=likes]").text(action.action_obj.check_sheets[i].likes);
            let fileID = action.action_obj.check_sheets[i].id;

            $("span[data-type=check_sheets][data-id=" + fileID + "][data-action=downloads]").html(action.action_obj.check_sheets[i].downloads);
            $("span[data-type=check_sheets][data-id=" + fileID + "][data-action=likes]").html(action.action_obj.check_sheets[i].likes);

            if (action.action_obj.check_sheets[i].is_like == true) {
                $("span[data-type=check_sheets][data-id=" + fileID + "][data-action=likes]").parent().find("a").removeClass("not_selected");
            } else {
                $("span[data-type=check_sheets][data-id=" + fileID + "][data-action=likes]").parent().find("a").addClass("not_selected");
            }
        }
    },
    subscribe_to_event: function () {
        $("span[data-type=check_sheets][data-action=likes]").parent().find("a").on("click", action.onLike);
        $(".like_img").on("dblclick", action.onLike);
    },

    onLike: function (event) {
        let $span = $(event.target).parent().parent().find("span.like_container");
        let data_type = $span.attr("data-type");
        let fileID = $span.attr("data-id");

        if (data_type && fileID) {
            action.update_like(data_type, fileID).then(function (result) {
                if (result.is_ok == true) {

                    $span.html(result.likes);

                    //меняем класс на противоположный 
                    $span.parent().find("a").toggleClass("not_selected");

                    //обновляем данные в глобальном массиве
                    if (data_type == "check_sheets") {
                        let el = action.action_obj.check_sheets.find(x => x.id == fileID);
                        el.is_like = result.is_like;
                        el.likes = result.likes;
                    }
                } else {
                    console.log("Ошибка при сохранения like");
                }

            });
        }

    },
    onDownload: function () {
        let $span = $(this).parent().find("span[data-id]");
        let data_type = $span.attr("data-type");
        let fileID = $span.attr("data-id");

        action.refresh_count_download(data_type, fileID).then(function (result) {
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
    update_like: function (data_type, fileID) {
        return $.ajax({
            url: '/Action/UpdateLike',
            type: "POST",
            data: { datatype: data_type, id: fileID },
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