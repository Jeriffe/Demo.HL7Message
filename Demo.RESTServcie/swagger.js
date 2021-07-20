'use strict';
window.SwaggerTranslator = {
    _words: [],

    translate: function () {
        var $this = this;
        $('[data-sw-translate]').each(function () {
            $(this).html($this._tryTranslate($(this).html()));
            $(this).val($this._tryTranslate($(this).val()));
            $(this).attr('title', $this._tryTranslate($(this).attr('title')));
        });
    },

    setControllerSummary: function () {

        try {
            console.log($("#input_baseUrl").val());
            $.ajax({
                type: "get",
                async: true,
                url: $("#input_baseUrl").val(),
                dataType: "json",
                success: function (data) {

                    var summaryDict = data.ControllerDesc;
                    console.log(summaryDict);
                    var id, controllerName, strSummary;
                    $("#resources_container .resource").each(function (i, item) {
                        id = $(item).attr("id");
                        if (id) {
                            controllerName = id.substring(9);
                            try {
                                strSummary = summaryDict[controllerName];
                                if (strSummary) {
                                    $(item).children(".heading").children(".options").first().prepend('<li class="controller-summary" style="color:green;" title="' + strSummary + '">' + strSummary + '</li>');
                                }
                            } catch (e) {
                                console.log(e);
                            }
                        }
                    });
                }
            });
        } catch (e) {
            console.log(e);
        }
    },
    _tryTranslate: function (word) {
        return this._words[$.trim(word)] !== undefined ? this._words[$.trim(word)] : word;
    },

    learn: function (wordsMap) {
        this._words = wordsMap;
    }
};

$(function () {
    window.SwaggerTranslator.translate();
    window.SwaggerTranslator.setControllerSummary();
});