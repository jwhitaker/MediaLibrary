define([
    'backbone',
    'collections/Genres'
], function (Backbone, GenreCollection) {
    var MovieModel = Backbone.Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Movies',
        defaults: {
            Title: '',
            YearReleased: '',
            Directors: '',
            Actors: '',
            Plot: '',
            Notes: ''
        },

        validate: function (attrs) {
            var errors = [];

            if (!attrs.Title) {
                errors.push({name: 'Title', message: 'A movie title is required' });
            }

            if (!attrs.YearReleased) {
                errors.push({ name: 'YearReleased', message: 'A year released value is required' });
            } else {
                var n = attrs.YearReleased.match(/^\d{4}$/);

                if (n == null) {
                    errors.push({ name: 'YearReleased', message: 'An invalid year was specified' });
                }
            }

            return errors.length > 0 ? errors : false;
        },

        parse: function (resp, options) {
            resp.Genres = new GenreCollection(resp.Genres);

            return resp;
        },

        toJSON: function (options) {
            var attributes = _.clone(this.attributes);

            if (attributes.Genres == null) {
                attributes.Genres = new GenreCollection();
            }

            attributes.Genres = attributes.Genres.toJSON();

            return attributes;
        }
    });

    return MovieModel;
});