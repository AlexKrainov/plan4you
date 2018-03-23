$(function () {
    bTable.init_table();
});

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