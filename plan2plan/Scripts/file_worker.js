$(function () {
    file_worker.init();
});
var bindDownload = null;
var file_worker = {
    file: {
        id: ""
    },
    init: function () {
        $("#download_dialog_container").load("../Dialogs/DownloadDialog.html");
    },
    open_download_dialog: function (obj) {
        file_worker.file.id = $(obj).closest("div[data-img-id]").attr("data-img-id");

        $('#downloadDialog').modal('show');
        bindDownload = action.onDownload.bind($(obj));
    },
    close_download_dialog: function () {
        $('#downloadDialog').modal('hide');
        file_worker.file.id = -1;
        //$("#person_mail").val("");
        $(".invalid-feedback").hide();
    },
    download_file: function () {
        let mail = $("#person_mail").val();

        if (isEmail(mail) == true) { //validation email
            file_worker.download_file_from_server(file_worker.file.id, mail)
                .then(function (result) {
                    if (result.is_ok == true) {
                        window.location.href = "/File/DownloadFile?id=" + file_worker.file.id + "&isLoad=true&mail=" + mail;

                        file_worker.close_download_dialog();
                        bindDownload();
                    } else {
                        alert("Извините, произошла ошибка при скачивании файле. Напишите нам и мы исправим.");
                        console.log(result);
                    }
                });
        } else {
            $(".invalid-feedback").show();
        }
    },
    //Получаем доступ на скачиваине файла и скачиваем файл
    download_file_from_server: function (id, mail) {
        if (!id) {
            return false;
        }
        return $.ajax({
            url: '/File/DownloadFile?id=' + id + '&isLoad=false&mail=' + mail,
            type: "GET",
            success: function (result) {
                return result;
            }
        });
    }

}