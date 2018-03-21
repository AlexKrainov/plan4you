Vue.component("vue-like", {
    template: ' <span><span>{{ count_like }}</span><a href="" onclick="return false;" v-on:click="click_vue()"><i class="fa fa-heart"></i></a></span>',
    props: ["data_action", "data_type", "data_id", "count_like"],
    data: function () {
        //var file = action.action_obj.find(x => x.id == data - id);
        //console.log(file);
        //this.count_like = file.likes
        return {};
    },
    methods: {
        click_vue: function () {
            var draw_likes_bind = this.draw_likes.bind(this);

            console.log(this.data_type);
            console.log(this.data_id);

            this.update_like(this.data_type, this.data_id).then(
                function (result) {
                    draw_likes_bind(result);
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
        draw_likes: function (obj) {
            //this.count_like = count;
            console.log(obj);
        }
    }
});