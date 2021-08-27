var URL_APPLICATION = $('#URL_APPLICATION').val();
$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
var removeUrlParameter = function removeUrlParameter(sParam) {
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
};


var sysFunctionConfig = {
    pageSize: $.cookie('pageSize') ? $.cookie('pageSize') : 5,
    pageIndex: 1,
}
var sysFunctionController = {
    init: function () {
        sysFunctionController.loadData();
        sysFunctionController.registerEvent();
    },
    registerEvent: function () {
        $('#frmSaveData').validate({
            rules: {
                txtFunctionName: "required",
                ddlTarget: "required",
                ddlTypeID: "required"
            },
            messages: {
                txtFunctionName: "Vui lòng nhập tên",
                ddlTarget: "Vui lòng chọn Target",
                ddlTypeID: "Vui lòng chọn vị trí"
            }
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            sysFunctionController.ChangeStatus(id, btn);
        });
        $('#filter_count_perpage').off('change').on('change', function () {
            var slt = $(this).val();
            $.cookie('pageSize', slt, { path: '/' });
            location.reload();
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            sysFunctionController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                sysFunctionController.saveData();
            }
        });
        $('.btn-role').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalRoles').modal('show');
            var id = $(this).data('id');
            sysFunctionController.loadDetailRole(id);
        });
        $('#btnSaveRole').off('click').on('click', function () {
            sysFunctionController.saveDataRole();
        });
        $('#btnSaveListRole').off('click').on('click', function () {
            sysFunctionController.saveDataMultiRole();
        });
        $('.btn-edit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            sysFunctionController.loadDetail(id);
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa bản ghi này không?", function (result) {
                if (result == true) {
                    sysFunctionController.deleteData(id);
                }
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            sysFunctionController.loadData(true);
        });
        $('#txtSearchS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                sysFunctionController.loadData(true);
            }
        });
        $('#btnReset').off('click').on('click', function () {
            $('#txtSearchS').val('');
            $('#ddlStatusS').val('true');
            sysFunctionController.loadData(true);
        });
        $('#CheckAll').off('change').on('change', function () {
            if (this.checked) {
                $(".checkSingle").each(function () {
                    this.checked = true;
                });
            } else {
                $(".checkSingle").each(function () {
                    this.checked = false;
                });
            }
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            var values = $(".checkSingle:checked").map(function () { return $(this).data('id'); }).get();
            if (values != "" && values != null) {
                $('#CheckAll').prop('checked', false);
                bootbox.confirm("Bạn có chắc chắn muốn xóa danh sách bản ghi này không?", function (result) {
                    if (result == true) {
                        sysFunctionController.deleteAllData(values);
                    }
                });
            } else {
                bootbox.alert("Chọn bản ghi cần xóa");
            }
        });
        $('#ddlColorModule').off('change').on('change', function () {
            var color = $(this).val();
            $('#BlockImgColor').attr('class', color);
            $('#BlockTextColor').attr('class', color);
            var strcolor = '<span class="' + color + '"><i class="fa fa-yelp"></i> ' + color + ' </span>';
            $("#select2-ddlColorModule-container").attr("title", color);
            $("#select2-ddlColorModule-container").html(strcolor);
        });
        $('#ddlIconsModule').off('change').on('change', function () {
            var icon = $(this).val();
            $('#IconModule').html(icon);
            var stricon = '<span><i class="material-icons f-left">' + icon + '</i> ' + icon + ' </span>';
            $("#select2-ddlIconsModule-container").attr("title", icon);
            $("#select2-ddlIconsModule-container").html(stricon);
        });
        $('#txtIcons').off('change').on('change', function () {
            var iconMenu = $(this).val();
            var striconMenu = '<span><i class="material-icons f-left">' + iconMenu + '</i> ' + iconMenu + ' </span>';
            $("#select2-txtIcons-container").attr("title", iconMenu);
            $("#select2-txtIcons-container").html(striconMenu);
        });
        $('#txtNameModule').on('keyup keydown', function () {
            $('#NameModule').html($(this).val());
        });
    },
    loadDetailRole: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#titleRoles').html('CẬP NHẬT ROLE');
                    $('#hidIDSysFunction').val(data.FunctionID);
                    $('#txtisController_Role').val(data.isController);
                    $('#txtRoleID_Role').val(data.RoleID);
                    var obj = JSON.parse(data.DanhSachRole);
                    var strDanhSachRole = "";
                    $.each(obj, function (i, item) {
                        strDanhSachRole += '<div class="alert alert-warning" style="padding: 5px;font-size: 10px;margin-bottom: 5px;">' + item.ID +'</div>';
                    });
                    $('#danh-sach-role').html(strDanhSachRole);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveDataRole: function () {
        var id = parseFloat($('#hidIDSysFunction').val());
        var RoleID = $('#txtRoleID_Role').val();
        var isController = $('#txtisController_Role').val();
        var sysfunction = {
            FunctionID: id,
            RoleID: RoleID,
            isController: isController
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/SaveDataRole',
            data: {
                strSysFunction: JSON.stringify(sysfunction)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    $('#modalRoles').modal('hide');
                    sysFunctionController.loadData(true);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveDataMultiRole: function () {
        var id = parseFloat($('#hidIDSysFunction').val());
        var RoleID = $('#txtRoleID_Role').val();
        var isController = $('#txtisController_Role').val();
        var sysfunction = {
            FunctionID: id,
            RoleID: RoleID,
            isController: isController
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/saveDataMultiRole',
            data: {
                strSysFunction: JSON.stringify(sysfunction)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    $('#modalRoles').modal('hide');
                    sysFunctionController.loadData(true);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteAllData: function (ids) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/DeleteMulti',
            data: {
                ids: ids
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    sysFunctionController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteData: function (id) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/Delete',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Xóa thành công", function () {
                    sysFunctionController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadDetail: function (id) {
        sysFunctionController.loadSelectParentId(0);
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#titleAddUpdate').html('CẬP NHẬT');
                    $('#hidID').val(data.FunctionID);
                    //$('#txtIcons').val(data.Icons);
                    $('#txtIcons').val(data.Icons).trigger('change');
                    var iconMenu = '<span><i class="material-icons f-left">' + data.Icons + '</i> ' + data.Icons + ' </span>';
                    $("#select2-txtIcons-container").attr("title", data.Icons);
                    $("#select2-txtIcons-container").html(iconMenu);
                    $('#txtFunctionName').val(data.FunctionName);
                    $('#txtLink').val(data.Link);
                    $('#ddlTarget').val(data.Target);
                    $('#ddlParentId').val(data.ParentId).trigger('change');
                    $('#txtDisplayOrder').val(data.DisplayOrder);
                    $('#ddlTypeID').val(data.TypeID);
                    $('#txtisController').val(data.isController);
                    $('#ddlisModule').val(data.isModule).trigger('change');
                    $('#txtRoleID').val(data.RoleID);
                    $('#chkStatus').prop('checked', data.Status);
                    $('#ddlColorModule').val(data.ColorModule).trigger('change');
                    var color = '<span class="' + data.ColorModule + '"><i class="fa fa-yelp"></i> ' + data.ColorModule + ' </span>';
                    $("#select2-ddlColorModule-container").attr("title", data.ColorModule);
                    $("#select2-ddlColorModule-container").html(color);
                    $('#BlockImgColor').attr('class', data.ColorModule);
                    $('#BlockTextColor').attr('class', data.ColorModule);
                    $('#txtNameModule').val(data.NameModule);
                    $('#NameModule').html(data.NameModule);
                    $('#ddlIconsModule').val(data.IconsModule).trigger('change');
                    var icon = '<span><i class="material-icons f-left">' + data.IconsModule + '</i> ' + data.IconsModule + ' </span>';
                    $("#select2-ddlIconsModule-container").attr("title", data.IconsModule);
                    $("#select2-ddlIconsModule-container").html(icon);
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveData: function () {
        var id = parseFloat($('#hidID').val());
        var icons = $('#txtIcons').val();
        var functionname = $('#txtFunctionName').val();
        var link = $('#txtLink').val();
        var target = $('#ddlTarget').val();
        var parentId = $('#ddlParentId').val();
        var DisplayOrder = $('#txtDisplayOrder').val();
        var typeid = $('#ddlTypeID').val();
        var isController = $('#txtisController').val();
        var isModule = $('#ddlisModule').val();
        var RoleID = $('#txtRoleID').val();
        var status = $('#chkStatus').prop('checked');
        var ColorModule = $('#ddlColorModule').val();
        var NameModule = $('#txtNameModule').val();
        var IconsModule = $('#ddlIconsModule').val();
        var sysfunction = {
            FunctionID: id,
            FunctionName: functionname,
            Link: link,
            Target: target,
            ParentId: parentId,
            DisplayOrder: DisplayOrder,
            Icons: icons,
            TypeID: typeid,
            isController: isController,
            isModule: isModule,
            RoleID: RoleID,
            Status: status,
            ColorModule: ColorModule,
            NameModule: NameModule,
            IconsModule: IconsModule
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/SaveData',
            data: {
                strSysFunction: JSON.stringify(sysfunction)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    //bootbox.alert("Lưu thành công", function () {
                    $('#modalAddUpdate').modal('hide');
                    sysFunctionController.loadData(true);
                    //});
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    resetForm: function () {
        var parentid = getUrlParameter('parentid');
        if (parentid == null || parentid == '' || parentid == 'undefined') {
            parentid = 0;
        }
        sysFunctionController.loadSelectParentId(parentid);
        $('#hidID').val('0');
        $('#txtIcons').val('');
        $('#txtFunctionName').val('');
        $('#txtLink').val('');
        $('#ddlTarget').val('_self');
        //setTimeout(function () {
        //    $('#ddlParentId').val(parentid).trigger('change');
        //}, 400);
        var intDisplayOrder = $('#total_count_row').html();
        try {
            intDisplayOrder = parseInt(intDisplayOrder) + 1;
        } catch (e) {

        }
        $('#txtDisplayOrder').val(intDisplayOrder);
        $('#ddlTypeID').val('2');
        $('#txtisController').val('');
        $('#ddlisModule').val(0).trigger('change');
        $('#txtRoleID').val('');
        $('#chkStatus').prop('checked', true);
        $('#titleAddUpdate').html('THÊM MỚI');
    },
    ChangeStatus: function (id, btn) {
        $.ajax({
            url: "/Admin/SysFunction/ChangeStatus",
            data: { id: id },
            dataType: "json",
            type: "POST",
            success: function (response) {
                if (response.status == true) {
                    btn.html('<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>');
                }
                else {
                    btn.html('<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>');
                }
            }
        });
    },
    loadSelectParentId: function (ParentId) {
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/loadSelectParentId',
            data: {
                ParentId: ParentId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="0">--- Chọn ---</option>';
                    $.each(response.data, function (i, item) {
                        var module = "";
                        if (item.isModule == 1) {
                            module = 'Biên Tập';
                        } else if (item.isModule == 2) {
                            module = 'Phê Duyệt';
                        } else if (item.isModule == 3) {
                            module = 'Quản Trị';
                        } else if (item.isModule == 4) {
                            module = 'Dự Án';
                        } else if (item.isModule == 5) {
                            module = 'Hệ Thống';
                        }else if (item.isModule == 6) {
                            module = 'Báo cáo';
                        }
                        html += '<option value="' + item.FunctionID + '">' + item.FunctionName + " [ " + module + '] </option>';
                    });
                    $('#ddlParentId').append(html);
                    $('#ddlParentId').val(ParentId).trigger('change');
                } else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function (changePageSize) {
        var functionname = $('#txtSearchS').val();
        var status = $('#ddlStatusS').val();
        var parentid = getUrlParameter('parentid');
        if (parentid == null || parentid == '' || parentid == 'undefined') {
            parentid = 0;
        }
        var sysfunction = {
            FunctionName: functionname,
            ParentId: parentid,
            Link: functionname,
            Status: status
        };
        $.ajax({
            url: URL_APPLICATION + '/Admin/SysFunction/LoadData',
            type: 'GET',
            data: {
                strSysFunction: JSON.stringify(sysfunction),
                page: sysFunctionConfig.pageIndex,
                pageSize: sysFunctionConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var grd_row = '';
                    var data = response.data;
                    var backParentId = response.parentid;
                    var html = '';
                    var template = $('#data-template-index').html();
                    $.each(data, function (i, item) {
                        grd_row = (i + 1);
                        var strStatus = '<span title="Kích hoạt" class="btn btn-success btn-xs"><i class="fa fa-check"></i ></span>';
                        if (item.Status == false) {
                            strStatus = '<span title="Không kích hoạt" class="btn btn-default btn-xs"><i class="fa fa-check"></i ></span>';
                        }
                        var typeid = "";
                        if (item.TypeID == 1) {
                            typeid = "Top";
                        } else if (item.TypeID == 2) {
                            typeid = "Left";
                        } else if (item.TypeID == 3) {
                            typeid = "Right";
                        } else if (item.TypeID == 4) {
                            typeid = "Bottom";
                        }
                        var module = "";
                        if (item.isModule == 1) {
                            module += '<button class="btn btn-round btn-xs btn-warning btn-block" title="BIÊN TẬP">';
                            module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> BIÊN TẬP </span>';
                            module += '</button >';
                        } else if (item.isModule == 2) {
                            module += '<button class="btn btn-round btn-xs btn-success btn-block" title="PHÊ DUYỆT">';
                            module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> PHÊ DUYỆT </span>';
                            module += '</button >';
                        } else if (item.isModule == 3) {
                            module += '<button class="btn btn-round btn-xs btn-info btn-block" title="QUẢN TRỊ">';
                            module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> QUẢN TRỊ </span>';
                            module += '</button >';
                        } else if (item.isModule == 4) {
                            module += '<button class="btn btn-round btn-xs btn-primary btn-block" title="DỰ ÁN">';
                            module += '<i class="fa fa-vcard-o"></i> <span class="hidden-phone" style="color: white"> DỰ ÁN </span>';
                            module += '</button >';
                        } else if (item.isModule == 5) {
                            module += '<button class="btn btn-round btn-xs btn-danger btn-block" title="HỆ THỐNG">';
                            module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> HỆ THỐNG </span>';
                            module += '</button >';
                        }
                        else if (item.isModule == 6) {
                            module += '<button class="btn btn-round btn-xs btn-danger btn-block" title="BÁO CÁO">';
                            module += '<i class="fa fa-gears"></i> <span class="hidden-phone" style="color: white"> BÁO CÁO </span>';
                            module += '</button >';
                        }
                        var FunctionName = "<a href='" +URL_APPLICATION + "/Admin/sysFunction/Index/?parentid=" + item.FunctionID + "'>" + item.FunctionName + "</a>";
                        var Controller = "";
                        if (item.isController != null && item.isController != "") {
                            Controller += '<div class="alert alert-success">';
                            Controller += "<div style='margin-top:10px;margin-bottom:10px;padding:0px 5px;width:200px;'>";
                            Controller += "<span class='position-relative' style='height:35px;padding-right:10px;'>";
                            Controller += item.isController;
                            Controller += "<small class='feedLblStyle lblReplyStyle' style='position:absolute;top:-12px;left:0px;font-size:8px;width:110px;'><i class='fa fa-circle'></i> Controller </small>";
                            Controller += "</span>";
                            Controller += "</div>";
                            Controller += '</div>';
                        }
                        var Link = "";
                        if (item.Link != "") {
                            Link += '<div class="alert alert-warning">' + item.Link + '</div>';
                        }
                        var IconHome = "";
                        if (item.IconsModule != null && item.IconsModule != "") {
                            IconHome += '<a class="slide btn-white" style="display: inline-block;position:relative;text-align: center;" href="javascript:;">';
                            IconHome += '<span class="' + item.ColorModule + '" ><i style="font-size: 50px;" class="material-icons partner-img">' + item.IconsModule + '</i></span >';
                            IconHome += '<span class="' + item.ColorModule + '"><p style="font-weight: bold;"><span>' + item.NameModule + '</span></p></span>';
                            IconHome += '</a >';
                        }
                        html += Mustache.render(template, {
                            STT: (i + 1),
                            FunctionID: item.FunctionID,
                            FunctionName: FunctionName,
                            Link: Link,
                            DisplayOrder: item.DisplayOrder,
                            Icons: item.Icons,
                            IconHome: IconHome,
                            RoleID: item.RoleID,
                            isController: Controller,
                            isModule: module,
                            Status: strStatus
                        });
                    });
                    $('#tblDataIndex').html(html);

                    $('#total_count_row').html(response.total);
                    $('#from_record').html((((sysFunctionConfig.pageIndex - 1) * sysFunctionConfig.pageSize) + 1));
                    $('#to_record').html((((sysFunctionConfig.pageIndex - 1) * sysFunctionConfig.pageSize) + grd_row));
                    $('#filter_count_perpage').val(sysFunctionConfig.pageSize);

                    try {
                        if (parentid != 0) {
                            var linkParentId = "<a href=URL_APPLICATION + '/Admin/sysFunction/Index/?parentid=" + backParentId + "'><i class=\"fa fa-arrow-circle-o-up\"></i> Lên một cấp </a>";
                            $('#link-parent-id').html(linkParentId);
                        }
                    } catch (e) {

                    }
                    sysFunctionController.paging(response.total, function () {
                        sysFunctionController.loadData();
                    }, changePageSize);
                    sysFunctionController.registerEvent();
                }
            }
        });


    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / sysFunctionConfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination-index a').length === 0 || changePageSize) {
            $('#pagination-index').empty();
            $('#pagination-index').removeData("twbs-pagination");
            $('#pagination-index').unbind("page");
        }

        $('#pagination-index').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: '<span aria-hidden="true">&laquo;</span>',
            next: 'Tiếp',
            last: '<span aria-hidden="true">&raquo;</span>',
            prev: 'Trước',
            onPageClick: function (event, page) {
                sysFunctionConfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
sysFunctionController.init();