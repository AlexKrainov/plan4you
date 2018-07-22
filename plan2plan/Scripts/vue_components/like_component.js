Vue.component("vue-like", {
    template: '<span><span>{{ count_like }}</span><a href="" onclick="return false;" v-on:click="onLike(this)" v-bind:class="{ not_selected: not_selected }"><i class="fa fa-heart"></i></a></span>',
    props: ["doctype", "id", "count_like", "not_selected"],
    data: function () {
        if (this.doctype == "check_sheets") {
            this.draw_likes();
        }
        return {};
    },
    methods: {
        onLike: function () {
            this.onLikeBind(this);
        },
        onLikeBind: function (_this) {
            $.ajax({
                url: '/Action/UpdateLike',
                type: "POST",
                data: { datatype: _this.doctype, id: _this.id },
                success: function (result) {
                    if (result.is_ok == true) {
                        let file = action.action_obj.check_sheets.find(x => x.id == _this.id);
                        file.is_like = result.is_like;
                        file.likes = result.likes;
                        _this.draw_likes();
                    }
                }
            });
        },
        draw_likes: function () {
            var file = action.action_obj.check_sheets.find(x => x.id == this.id);
            this.count_like = file.likes
            this.not_selected = !file.is_like;
        }
    }
});