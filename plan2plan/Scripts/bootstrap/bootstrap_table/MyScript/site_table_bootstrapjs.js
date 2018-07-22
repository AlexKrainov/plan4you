$(function () {
    if ($('#file_table').length != 0) {
        bTable.init_table();
    } else if ($("#tbl-statistics").length != 0) {
        tbl-statistics.draw_table();
    }

});
//Для таблицы на странице файлов
var bTable = {
    $table: null,
    init_table: function () {
        bTable.draw_table();
    },
    draw_table: function () {
        //http://bootstrap-table.wenzhixin.net.cn/documentation/#table-options
        bTable.$table = $('#file_table').bootstrapTable(
            {
                toolbar: "#toolbar",
                search: true,
                pagination: true,
                showToggle: true,
                //resizable: true,
                //stickyHeader: true,
            }
        );

        //добавляем события
        //table.$table.on('dbl-click-row.bs.table', table.dblclick);
        //table.$table.on('click-row.bs.table', table.select_row);
    }
};

//Для таблицы на странице статистики

var tbl-statistics = {
    $table: null,
    draw_table: function () {
        //http://bootstrap-table.wenzhixin.net.cn/documentation/#table-options
        bTable.$table = $('#tbl-statistics').bootstrapTable(
            {
                //toolbar: "#toolbar",
                search: true,
                pagination: true,
                pagination: true,
                showToggle: true,
                //switchable: true,
                showColumns: true
                //resizable: true,
                //stickyHeader: true,
            }
        );

        //В bootstrap 4 убрали иконки .glyphicon, так что меняем на другие
        $(".glyphicon.glyphicon-th").attr("class", "fa fa-list");
        $("<i class='fa fa-align-justify'></i>").appendTo($("button[aria-label=toggle]"));

    }
}