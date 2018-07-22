$(function () {
    stat_vue.init_page();
});

var stat_vue = new Vue({
    el: "#stat_vue",
    data: {
        columns: {
            ID: 0,
            dateTime: 1,
            IP: 2,
            Browser_name: 3,
            OS_name: 4,
            Country: 5,
            Status: 6,
            isMobile: 7,
            Referrer: 8,
            Screen_size: 9,
        },
        dateFormatForTable: "DD.MM.YYYY HH:mm"
    },
    computed: {
        dataTable() {
            return $("#tbl-statistics").DataTable({
                paging: true,
                ordering: true,
                stateSave: true,
                searching: true,
                //pageLength: 50,
                order: [[this.columns.ID, "asc"]],
                //dom: '<"row"><"col-md-5"i><"col-md-3"l><"col-md-4"f><"bottom"tp>', //<"wrapper"f>il
                language: {
                    search: "Search within results:"
                },
                columns: [
					{
					    render: function (data, type, full, meta) {
					        let result = "";

					        if (full.isMobile == true) {
					            result += '<i class="fa fa-mobile" aria-hidden="true" title="is mobile"></i>';
					        }
					        return full.ID + '<span class="pull-right">' + result + '</span>';
					    }
					},
					{
					    render: function (data, type, full, meta) {
					        let date = moment(full.dateTime);
					        if (date.isValid() == true) {
					            return date.format(stat_vue.dateFormatForTable);
					        } else {
					            return "";
					        }
					    }
					},
					{
					    render: function (data, type, full, meta) {
					        return full.IP;
					    }
					},
                    {
                        render: function (data, type, full, meta) {
                            return full.Browser_name + " " + full.Browser_version;
                        }
                    },
                     {
                         render: function (data, type, full, meta) {
                             return full.OS_name + " " + full.OS_version;
                         }
                     },
                     {
                         render: function (data, type, full, meta) {
                             return full.Country + " " + full.City;
                         }
                     },
                       {
                           render: function (data, type, full, meta) {
                               if (full.Status == "New") {
                                   return '<span class="label label-primary">New</span>';
                               } else if (full.Status == "Return") {
                                   return '<span class="label label-success">Return</span>'
                               }
                               return full.Status;
                           }
                       },
                        {
                            render: function (data, type, full, meta) {
                                return full.Referrer;
                            }
                        },
                         {
                             render: function (data, type, full, meta) {
                                 return full.Screen_size;
                             }
                         }
                ],
                columnDefs: [
				{
				    targets: "td-text-center",
				    className: "text-center"
				},
				{
				    targets: "person-width-column",
				    width: "10%"
				}, ]
            });
        },
    },
    methods: {
        init_page() {
            this.init_table();
        },
        init_table() {
            this.dataTable.clear();
            this.dataTable.draw(false);

            // #region color for filter icon

            //let filterData = $("#vehicleModel_modal_filter form").serializeObject();
            //if (filterData.cartypeIDs == undefined && filterData.markIDs == undefined) {
            //    this.isDirtyFilter = false;
            //} else {
            //    this.isDirtyFilter = true;
            //}

            // #endregion

            return $.ajax({
                type: "POST",
                url: "/Statistics/GetTableData",
                //data: JSON.stringify(filterData),
                contentType: "application/json",
                dataType: 'json',
                success: function (response) {
                    stat_vue.dataTable.clear();
                    stat_vue.dataTable.rows.add(response.data);
                    stat_vue.dataTable.draw(false);

                    // localStorage.setItem(stat_vue.localStorageString, JSON.stringify($("#vehicleModel_modal_filter form").serializeObject()));
                },
                error: function (xhr, status, error) { console.log(error); }
            });
        }
    }
});