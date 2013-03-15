define([
    'backbone',
    'text!templates/header.html'
], function (Backbone, headerTemplate) {
    var HeaderView = Backbone.View.extend({
        initialize: function () {
            this.template = _.template(headerTemplate);
        },

        render: function () {
            $(this.el).html(this.template());

            return this;
        }
    });

    return HeaderView;
});