﻿; (function (define) {
    define(['jQuery'], function ($) {
        
        var $modal;
        var defaultSettings;
        var options = {};
        var lastParams = {};

        var tmpModalContent = 'tmp-modal-content';
        var recModalContent = 'rec-modal-content';
        var bin = 'recycle-bin';
        var div = '<div>';

        return {
            ajax: ajax,
            alert: alert,
            confirm: confirm,
            emptyBin: emptyBin,
            prompt: prompt,
            setEModalOptions: setEModalOptions,
            setModalOptions: setModalOptions
        };
        
        function ajax(data, title) {
           
            var params = {
                loading: true,
                url: data.url || data
            };

            if (data.url) {
                $.extend(params, data);
            }

            return alert(params, title)
                .find('.modal-body')
                .load(params.url, error)
                .closest('.modal-dialog');

            function error(responseText, textStatus) {
                if (textStatus == 'error') {
                    alert('Url [ ' + params.url + ' ] load fail.', 'Loading: ' + (params.title || title));
                }
            }
        }
        function alert(params, title) {
           
            setup(params, title);

            var $message = $('<section>').append(getMessage(params), getFooter(params.buttons));

            return build($message, params);
        }
        function confirm(params, title, callback) {
            
            var label = {
                OK: 'Cancel',
                Yes: 'No',
                True: 'False'
            };
            var defaultLable = 'OK';

            return alert({
                message: params.message || params,
                title: params.title || title,
                onHide: click,
                size: params.size,
                buttons: [
                    { close: true, text: label[params.label] ? label[params.label] : label[defaultLable], style: 'danger' },
                    { close: true, click: click, text: label[params.label] ? params.label : defaultLable }
                ]

            });

            function click(ev) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="ev"></param>
                /// <returns type=""></returns>

                var fn = params.callback || callback;

                $modal.off("hide.bs.modal");

                if (typeof fn === 'function') {
                    var key = $(ev.currentTarget).html();
                    return fn(label[key] ? true : false);
                }

                throw new Error('No callback provided to execute confim modal action.');
            }
        }
        function emptyBin() {
            /// <summary>
            /// 
            /// </summary>

            return $('#' + bin + ' > *').remove();
        }
        function prompt(data, title, callback) {

            var params = {};
            if (typeof data === 'object') {
                $.extend(params, data);
            }
            else {
                params.message = data;
                params.title = title;
                params.callback = callback;
            }

            params.buttons = false;
            params.onHide = submit;

            var buttons = getFooter([
                { close: true, type: 'reset', text: 'Cancel', style: 'danger' },
                { close: false, type: 'submit', text: 'OK' }
            ]);

            params.message = $('<form role=form style="margin-bottom: 0;">' +
                '<div  class=modal-body>' +
                '<label for=prompt-input class=control-label>' + (params.message || '') + '</label>' +
                '<input type=text class=form-control id=prompt-input required autofocus value="' + (params.value || '') + (params.pattern ? '" pattern="' + params.pattern : "") + '">' +
                '</div></form>')
                .append(buttons)
                .on("submit", submit);

            return alert(params);

            function submit(ev) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="ev"></param>
                /// <returns type=""></returns>

                var fn = params.callback || callback;

                $modal.off("hide.bs.modal").modal('hide');

                if (typeof fn === 'function') {
                    return fn($(ev.currentTarget).html() === 'Cancel' ? null : $modal.find('input').val());
                }

                throw new Error('No callback provided to execute prompt modal action.');
            }

        }
        function setEModalOptions(overrideOptions) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="overrideOptions"></param>
            /// <returns type=""></returns>

            !defaultSettings && (defaultSettings = getDefaultSettings());

            return $.extend(defaultSettings, overrideOptions);
        }
        function setModalOptions(overrideOptions) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="overrideOptions"></param>
            /// <returns type=""></returns>

            $modal && $modal.remove();

            return $.extend(options, overrideOptions);
        }
        //#endregion

        //#region Private Logic
        function build(message, params) {

            $modal.find('.modal-content')
                .append(message.addClass(params.useBin && !params.loading ? recModalContent : tmpModalContent));

            return $modal.modal(options);
        }
        function getDefaultSettings() {
            return {
                allowContentRecycle: true,
                size: '',
                loadingHtml: '<div class=progress><div class="progress-bar progress-bar-striped active" role=progressbar aria-valuenow=100 aria-valuemin=0 aria-valuemax=100 style="width: 100%"><span class=sr-only>100% Complete</span></div></div>',
                title: 'Attention'
            };
        }
        function getModalInstance() {
            /// <summary>
            /// Return a new modal object if is the first request or the already created model.
            /// </summary>
            /// <returns type="jQuery Object">The model instance.</returns>

            if (!$modal) {
                //add recycle bin container to document
                if (!document.getElementById(bin)) {
                    $('body').append($(div).prop('id', bin).hide());
                }

                defaultSettings = getDefaultSettings();
                $modal = createModalElement();
            }

            return $modal;

            function createModalElement() {
                /// <summary>
                /// 
                /// </summary>
                /// <returns type="jQuery object"></returns>

                return $('<div class="modal fade">'
                    + ' <div class=modal-dialog>'
                    + '  <div class=modal-content>'
                    + '   <div class=modal-header><button type=button class="x close" data-dismiss=modal><span aria-hidden=true>&times;</span><span class=sr-only>Close</span></button><h4 class=modal-title></h4></div>'
                    + '  </div>'
                    + ' </div>'
                    + '</div>')
                    .on('hidden.bs.modal', recycleModal);
            }
        }
        function getFooter(buttons) {
            /// <summary>
            /// 
            /// </summary>
            /// <returns type=""></returns>

            if (buttons === false) {
                return '';
            }

            var messageFotter = $(div).addClass('modal-footer');
            if (buttons) {
                for (var i = 0, k = buttons.length; i < k; i++) {
                    var btnOp = buttons[i];
                    var btn = $('<button>').addClass('btn btn-' + (btnOp.style || 'info'));

                    for (var index in btnOp) {
                        if (btnOp.hasOwnProperty(index)) {
                            switch (index) {
                                case 'close':
                                    //add close event
                                    if (btnOp[index]) {
                                        btn.attr('data-dismiss', 'modal')
                                            .addClass('x');
                                    }
                                    break;
                                case 'click':
                                    //click event
                                    btn.click(btnOp.click);
                                    break;
                                case 'text':
                                    btn.html(btnOp[index]);
                                    break;
                                default:
                                    //all other possible html attributes to button element
                                    btn.attr(index, btnOp[index]);
                            }
                        }
                    }

                    messageFotter.append(btn);
                }
            } else {
                //if no buttons defined by user, add a standard close button.
                messageFotter.append('<button class="x btn btn-info" data-dismiss=modal type=button>Close</button>');
            }
            return messageFotter;
        }
        function getMessage(params) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="params"></param>
            /// <returns type=""></returns>

            var $message;
            var content = params.loading
                ? defaultSettings.loadingHtml
                : (params.message || params);

            if (content.on || content.onclick)
                $message = params.clone === true
                    ? $(content).clone()
                    : $(content);
            else {
                $message = $(div).addClass('modal-body')
                    .html(content);
            }

            return params.css && (params.css != $message.css && $message.css(params.css)), $message;
        }
        function recycleModal() {
            /// <summary>
            /// Move content to recycle bin if is a DOM object defined by user,
            /// delete itar if is a simple string message.
            /// All modal messages can be deleted if default setting "allowContentRecycle" = false.
            /// </summary>

            if (!$modal) return $modal;

            $modal//.removeAttr("style")
                .off("hide.bs.modal")
                .find('.' + tmpModalContent + (!defaultSettings.allowContentRecycle || lastParams.clone ? ', .' + recModalContent : ''))
                .remove();

            return $modal
                .find('.' + recModalContent).removeClass(recModalContent)
                .appendTo('#' + bin);

            // closeOpenPopover();
            // closeOpenTooltips);
        }
        function setup(params, title) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name='params'></param>
            /// <param name='title'></param>
            /// <returns type=''></returns>

            if (!params) throw new Error('Invalid parameters!');

            recycleModal();
            lastParams = params;

            // Lazy loading
            var $ref = getModalInstance();

            //#region change size
            $ref.find('.modal-dialog')
                .removeClass('modal-sm modal-lg')
                .addClass(params.size ? 'modal-' + params.size : defaultSettings.size);
            //#endregion

            //#region change title
            $ref.find('.modal-title')
                .html((params.title || title || defaultSettings.title) + ' ')
                .append($('<small>').html(params.subtitle || ''));
            //#endregion

            $ref.on("hide.bs.modal", params.onHide);
            return $ref;
        }
        //#endregion
    });

    /// AMD Module legacy creation
}(typeof define == 'function' && define.amd ? define : function (n, t) {
    typeof module != 'undefined' && module.exports ? module.exports = t(require(n[0])) : window.eModal = t(window.jQuery);
}));