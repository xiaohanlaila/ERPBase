//判断是否空或者空字符串
function IsNullOrEmpty(obj) {
    if (obj == undefined) {
        return true;
    }

    if (obj == null) {
        return true;
    }

    if (obj == "") {
        return true;
    }
    return false;
}