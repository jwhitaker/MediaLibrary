define([
    'backbone',
    'text!templates/home/homeTemplate.html'
], function (Backbone, homeTemplate) {
    var HomeView = Backbone.View.extend({
        render: function () {
            this.$el.html(homeTemplate);

            return this;
        }
    });

    return HomeView;
});