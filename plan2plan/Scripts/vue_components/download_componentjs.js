Vue.component("vue-download", {
    template: '<span><a href="" onclick="file_worker.open_download_dialog(this); return false;"title="Скачать"><i class="fa fa-download"></i></a><span v-bind:data-id="id" v-bind:doctype="doctype" data-action="downloads" >{{ count_download }}</span></span>',
    props: ["id", "count_download", "doctype"],
    data: function () {
        this.draw_download();
        return {};
    },
    methods: {
        onDownload: function () {

        },
        draw_download: function () {
            var file = action.action_obj.check_sheets.find(x => x.id == this.id);
            this.count_download = file.downloads;
        }
    }

});