var URL_APPLICATION = $('#URL_APPLICATION').val();
var SearchController = {
    init: function () {
        SearchController.loadThuongHieu();
        SearchController.loadQuocGia();
        SearchController.loadChatLieu();
        SearchController.loadTienIch();
        SearchController.registerEvent();
    },
    registerEvent: function () {
        
    },
    loadThuongHieu: function () {
        try {
            var url = URL_APPLICATION + '/Product/getUnit';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    json: ""
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var units = response.data;
                        var html = '';
                        var templateThuongHieu = $('#template-search-thuonghieu').html();
                        $.each(units, function (i, item) {
                            try {
                                var id = item.Id;
                                var images = URL_APPLICATION + "/Data/Logo/logo.png";
                                if (item.Icons != "" && item.Icons != null) {
                                    images = URL_APPLICATION + item.Icons;
                                }
                                var Title = item.Title;
                                var MetaTitle = item.MetaTitleUnit;
                                html += Mustache.render(templateThuongHieu, {
                                    id: id,
                                    MetaTitle: MetaTitle,
                                    Title: Title,
                                    images: images
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataThuongHieu').html(html);
                        $('#tblDataThuongHieuBoLoc').html(html);
                        SearchController.registerEvent();
                    }
                },
                error: function (data, Status) {
                    console.log(data.responseText);
                }
            });
        } catch (e) {
            // TODO: handle exception
        }
    },
    loadQuocGia: function () {
        try {
            var url = URL_APPLICATION + '/Product/getCountry';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    json: ""
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var countrys = response.data;
                        var html = '';
                        var templateQuocGia = $('#template-search-quocgia').html();
                        $.each(countrys, function (i, item) {
                            try {
                                var id = item.Id;
                                var Name = item.Name;
                                html += Mustache.render(templateQuocGia, {
                                    id: id,
                                    Name: Name
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataQuocGia').html(html);
                        SearchController.registerEvent();
                    }
                },
                error: function (data, Status) {
                    console.log(data.responseText);
                }
            });
        } catch (e) {
            // TODO: handle exception
        }
    },
    loadChatLieu: function () {
        try {
            var url = URL_APPLICATION + '/Product/getMaterial';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    json: ""
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var datas = response.data;
                        var html = '';
                        var template = $('#template-search-chatlieu').html();
                        $.each(datas, function (i, item) {
                            try {
                                var id = item.Id;
                                var Name = item.Name;
                                html += Mustache.render(template, {
                                    id: id,
                                    Name: Name
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataChatLieu').html(html);
                        $('#tblDataChatLieuBoLoc').html(html);
                        SearchController.registerEvent();
                    }
                },
                error: function (data, Status) {
                    console.log(data.responseText);
                }
            });
        } catch (e) {
            // TODO: handle exception
        }
    },
    loadTienIch: function () {
        try {
            var url = URL_APPLICATION + '/Product/getUtility';
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    json: ""
                },
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        var datas = response.data;
                        var html = '';
                        var template = $('#template-search-tienich').html();
                        $.each(datas, function (i, item) {
                            try {
                                var id = item.Id;
                                var Name = item.Name;
                                html += Mustache.render(template, {
                                    id: id,
                                    Name: Name
                                });
                            } catch (e) {

                            }
                        });

                        $('#tblDataTienIch').html(html);
                        $('#tblDataTienIchBoLoc').html(html);
                        SearchController.registerEvent();
                    }
                },
                error: function (data, Status) {
                    console.log(data.responseText);
                }
            });
        } catch (e) {
            // TODO: handle exception
        }
    }
}
SearchController.init();