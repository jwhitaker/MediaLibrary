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
            Notes: '',
            IMDB: '',
            PosterFilename: ''
        },

        initialize: function () {
            if (this.attributes.Genres === undefined) {
                this.attributes.Genres = new GenreCollection();
            }

            this.validators = {};

            this.validators.Title = function (value) {
                return value.length > 0 ? { isValid: true } : { isValid: false, message: 'A title is required' };
            };

            this.validators.YearReleased = function (value) {
                var message = "";

                if (!value) {
                    message = "A year is required";
                } else {
                    if (value.match(/^\d{4}$/) == null) {
                        message = "An invalid year was specified";
                    }
                }

                if (message.length > 0) {
                    return { isValid: false, message: message };
                }

                return { isValid: true };
            };
        },

        validateItem: function (key) {
            return (this.validators[key]) ? this.validators[key](this.get(key)) : { isValid: true };
        },

        validate: function (attrs) {
            var messages = {};

            for (key in this.validators) {
                if (this.validators.hasOwnProperty(key)) {
                    var check = this.validators[key](this.get(key));
                    if (check.isValid === false) {
                        messages[key] = check.message;
                    }
                }
            }

            return _.size(messages) > 0 ? messages : false;
        },

        parse: function (resp, options) {
            resp.Genres = new GenreCollection(resp.Genres);

            return resp;
        },

        toJSON: function (options) {
            var attributes = _.clone(this.attributes);

            attributes.Genres = attributes.Genres.toJSON();

            return attributes;
        }
    });

    return MovieModel;
});