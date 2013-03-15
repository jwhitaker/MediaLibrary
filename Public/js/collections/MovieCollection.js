define([
    'backbone',
    'models/Movie'
], function (Backbone, Movie) {
    var MovieCollection = Backbone.Collection.extend({
        model: Movie,

        url: function () {
            var url = 'api/Movies?' + $.param({
                page: this.page,
                itemsPerPage: this.itemsPerPage
            });

            return url;
        },

        fetch: function (options) {
            options || (options = {});

            this.page = options.page || (options.page = 1);
            this.itemsPerPage = options.itemsPerPage || (options.itemsPerPage = 5);

            Backbone.Collection.prototype.fetch.call(this, options);
        },

        getInfo: function () {
            var info = {
                currentPage: this.page,
                totalItems: this.totalItems,
                itemsPerPage: this.itemsPerPage,
                totalPages: Math.ceil(this.totalItems / this.itemsPerPage)
            };

            info['prevPage'] = info.currentPage > 1 ? info.currentPage - 1 : null;
            info['nextPage'] = info.currentPage < info.totalPages ? info.currentPage + 1 : null;

            return info;
        },

        parse: function (resp) {
            this.totalItems = resp.TotalItems;
            this.page = resp.Page;

            return resp.Items;
        }
    });

    return MovieCollection;
});