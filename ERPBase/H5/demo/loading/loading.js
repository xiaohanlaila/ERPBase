/***
 * loading v1.0.0
 * data 2019-8-19
 * auth whd <249605509@qq.com>
 * @param {object} options loading配置
 * @examples var loading1 = new Loading()
 *           loading1.show()
 *           loading1.hide()
 * @constructor
 */

function Loading(options) {
  var options = options || {}
  this.text = (options.text || '正在加载').slice(0, 4); //loading文字
  this.id = options.id || this._UUID(8); //loading ID
  this.backgroundColor = options.backgroundColor || 'rgba(0, 0, 0, .3)'; //overlay背景颜色
  this.isBackground = options.isBackground || false;  //是否显示overlay
  this.littleBackgroundColor = options.littleBackgroundColor || 'rgba(0, 0, 0, .8)'; // loading背景色
  this.D = parseFloat(options.D) || 30; //loading 直径
  this.D = this.D > 40 ? 40 : this.D;
  this.D = this.D < 25 ? 25 : this.D;
  this.RColor = options.RColor || '#eee'; // 轨道颜色
  this.RActiveColor = options.RActiveColor || '#5395ff'; //滑块颜色
  this.RTime = parseFloat(options.RTime) || 1.5; //动画时间

  this._init()
}
Loading.prototype = {
  constructor: Loading,

  //显示loading
  show: function() {
    document.body.appendChild(this.loading)
  },

  //隐藏loading
  hide: function(id) {
    if (id) {
      var el = document.querySelector('#' + id);
      if(el) document.body.removeChild(el);
    } else {
      document.body.removeChild(this.loading);
    }
  },
  _init: function() {
    this._createEl();
    if (!Loading.sty) this._addStyle()
  },
  //创建元素
  _createEl: function () {
    this.loading = document.createElement('div')
    this.loading.className = 'loading';
    this.loading.id = this.id;
    if (this.isBackground) {
      this.loading.innerHTML = '<div class="overlay"></div>\n' +
        '  <div class="loading-box">\n' +
        '    <div class="loading-circle"></div>\n' +
        '    <div class="loading-text">正在加载</div>\n' +
        '</div>'
    } else {
      this.loading.innerHTML = '<div class="loading-box">\n' +
        '    <div class="loading-circle"></div>\n' +
        '    <div class="loading-text">正在加载</div>\n' +
        '</div>'
    }
  },

  //加添样式
  _addStyle: function () {
    var sty = document.createElement('style')
    sty.type = 'text/css';
    sty.innerHTML='.overlay {\n' +
      '      position: fixed;\n' +
      '      top: 0;\n' +
      '      left: 0;\n' +
      '      bottom: 0;\n' +
      '      right: 0;\n' +
      '      background: ' + this.backgroundColor + ';\n' +
      '    }\n' +
      '    .loading-box {\n' +
      '      position: fixed;\n' +
      '      background: ' + this.littleBackgroundColor + ';\n' +
      '      border-radius: 5px;\n' +
      '      text-align: center;\n' +
      '      padding: 15px 18px;\n' +
      '      left: 50%;\n' +
      '      top: 38.2%;\n' +
      '      transform: translateX(-50%);\n' +
      '    }\n' +
      '    .loading-box .loading-circle{\n' +
      '      display: inline-block;\n' +
      '      box-sizing: border-box;\n' +
      '      width: ' + this.D + 'px;\n' +
      '      height: ' + this.D + 'px;\n' +
      '      border: 4px solid ' + this.RColor + ';\n' +
      '      vertical-align: middle;\n' +
      '      border-radius: 50%;\n' +
      '      border-left-color: ' + this.RActiveColor + ';\n' +
      '      animation: circle ' + this.RTime + 's linear infinite;\n' +
      '    }\n' +
      '\n' +
      '    @keyframes circle {\n' +
      '      0% {\n' +
      '        -webkit-transform: rotate(0deg);\n' +
      '        -moz-transform: rotate(0deg);\n' +
      '        -ms-transform: rotate(0deg);\n' +
      '        -o-transform: rotate(0deg);\n' +
      '        transform: rotate(0deg);\n' +
      '      }\n' +
      '      100% {\n' +
      '        -webkit-transform: rotate(360deg);\n' +
      '        -moz-transform: rotate(360deg);\n' +
      '        -ms-transform: rotate(360deg);\n' +
      '        -o-transform: rotate(360deg);\n' +
      '        transform: rotate(360deg);\n' +
      '      }\n' +
      '    }\n' +
      '    @-webkit-keyframes circle {\n' +
      '      0% {\n' +
      '        -webkit-transform: rotate(0deg);\n' +
      '        -moz-transform: rotate(0deg);\n' +
      '        -ms-transform: rotate(0deg);\n' +
      '        -o-transform: rotate(0deg);\n' +
      '        transform: rotate(0deg);\n' +
      '      }\n' +
      '      100% {\n' +
      '        -webkit-transform: rotate(360deg);\n' +
      '        -moz-transform: rotate(360deg);\n' +
      '        -ms-transform: rotate(360deg);\n' +
      '        -o-transform: rotate(360deg);\n' +
      '        transform: rotate(360deg);\n' +
      '      }\n' +
      '    }.loading-box .loading-text {\n' +
      '      font-size: 14px;\n' +
      '      color: #fff;\n' +
      '      margin-top: 5px;\n' +
      '    }'

    document.querySelector('head').appendChild(sty)
    Loading.sty = true
  },

  _UUID: function (len, radix) {
    var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
    var uuid = [], i;
    radix = radix || chars.length;

    if (len) {
      for (i = 0; i < len; i++) uuid[i] = chars[0 | Math.random()*radix];
    } else {
      var r;
      uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
      uuid[14] = '4';
      for (i = 0; i < 36; i++) {
        if (!uuid[i]) {
          r = 0 | Math.random()*16;
          uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
        }
      }
    }
    return uuid.join('');
  }
}