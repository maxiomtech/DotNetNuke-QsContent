var qsContentRegisteredModules = [];
(function ($) {
    $(document).ready(function () {
        if (qsContentRegisteredModules.length > 0) {
            $(qsContentRegisteredModules[0]).each(function() {
                var checkValidation = this;
                if (!checkValidation.Visible) {
                    $('.DnnModule-' + checkValidation.ModuleID).hide();
                }
            });
        }
    });
})(jQuery);