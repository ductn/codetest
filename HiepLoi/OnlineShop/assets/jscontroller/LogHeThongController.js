var URL_APPLICATION = $('#URL_APPLICATION').val();
var LogHeThongConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var LogHeThong = {
    init: function () {
        LogHeThong.loadData();
        LogHeThong.registerEvents();
    },
    registerEvents: function () {
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            var href = LogHeThong.removeParams('page').replace('=undefined', '');
            window.location.href = href;
        });
    },
    loadData: function () {
        $('#filter_count_perpage').val(LogHeThongConfig.pageSize);
    },
    removeParams: function (sParam) {
        var url = window.location.href.split('?')[0] + '?';
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;
        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] != sParam) {
                url = url + sParameterName[0] + '=' + sParameterName[1] + '&'
            }
        }
        return url.substring(0, url.length - 1);
    }
}
LogHeThong.init();