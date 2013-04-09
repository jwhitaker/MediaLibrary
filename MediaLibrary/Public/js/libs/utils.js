!function ($) {
    Array.prototype.chunk = function (chunkSize) {
        var array = this;
        var lists = _.groupBy(array, function (a, b) {
            return Math.floor(b / chunkSize);
        });
        
        return _.toArray(lists);
    };

    Array.prototype.transpose = function () {
        var a = this,
            w = a.length ? a.length : 0,
            h = a[0] instanceof Array ? a[0].length : 0;

        if (h === 0 || w === 0) { return []; }

        var i, j, t = [];

        for (i = 0; i < h; i++) {
            t[i] = [];

            for (j = 0; j < w; j++) {
                t[i][j] = a[j][i];
            }
        }

        return t;
    };

    window.utils = {
        showAlert: function (title, message, klass) {
            $('.alert').removeClass("alert-error alert-warning alert-success alert-info");
            $('.alert').addClass(klass);
            $('.alert').html("<b>" + title + "</b> " + message);
            $('.alert').show();
        },

        hideAlert: function () {
            $('.alert').hide();
        },

        displayFieldError: function (error) {
            var field = error.name + '-input';

            var controlGroup = $('#' + field).parent().parent();
            controlGroup.addClass('error');

            $('.help-inline', controlGroup).html(error.message);
        },

        removeFieldError: function (name) {
            var field = name + '-input';
            var controlGroup = $('#' + field).parent().parent();
            controlGroup.removeClass('error');
            $('.help-inline', controlGroup).html('');
        },

        uploadPoster: function (file, callback) {
            var formData = new FormData();
            formData.append('poster', file);

            $.ajax({
                url: '/api/Poster',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data instanceof Array) {
                        callback(data[0].FileName);
                    } else {
                        alert('The server did not return the correct data after uploading the poster');
                    }
                }
            });
        }
    };
}(window.jQuery);