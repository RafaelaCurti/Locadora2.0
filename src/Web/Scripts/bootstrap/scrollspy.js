/* =============================================================
 * bootstrap-scrollspy.js v2.0.2
 * http://twitter.github.com/bootstrap/javascript.html#scrollspy
 * =============================================================
 * Copyright 2012 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ============================================================== */

!function ($) {

    "use strict"

    /* SCROLLSPY CLASS DEFINITION
     * ========================== */

    function ScrollSpy(element, options) {
        var process = $.proxy(this.process, this)
            , $element = $(element).is('body') ? $(window) : $(element)
            , href
        this.options = $.extend({}, $.fn.scrollspy.defaults, options)
        this.$scrollElement = $element.on('scroll.scroll.data-api', process)
        this.selector = (this.options.target
            || ((href = $(element).attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')) //strip for ie7
            || '') + ' .nav li > a'
        this.$body = $('body').on('click.scroll.data-api', this.selector, process)
        this.refresh()
        this.process()
    }

    ScrollSpy.prototype = {

        constructor: ScrollSpy

        , refresh: function () {
            this.targets = this.$body
                .find(this.selector)
                .map(function () {
                    var href = $(this).attr('href')
                    return /^#\w/.test(href) && $(href).length ? href : null
                })

            this.offsets = $.map(this.targets, function (id) {
                return $(id).position().top
            })
        }

        , process: function () {
            var scrollTop = this.$scrollElement.scrollTop() + this.options.offset
                , offsets = this.offsets
                , targets = this.targets
                , activeTarget = this.activeTarget
                , i

            for (i = offsets.length; i--;) {
                activeTarget != targets[i]
                    && scrollTop >= offsets[i]
                    && (!offsets[i + 1] || scrollTop <= offsets[i + 1])
                    && this.activate(targets[i])
            }
        }

        , activate: function (target) {
            var active

            this.activeTarget = target

            this.$body
                .find(this.selector).parent('.active')
                .removeClass('active')

            active = this.$body
                .find(this.selector + '[href="' + target + '"]')
                .parent('li')
                .addClass('active')

            if (active.parent('.dropdown-menu')) {
                active.closest('li.dropdown').addClass('active')
            }
        }

    }


    /* SCROLLSPY PLUGIN DEFINITION
     * =========================== */

    $.fn.scrollspy = function (option) {
        return this.each(function () {
            var $this = $(this)
                , data = $this.data('scrollspy')
                , options = typeof option == 'object' && option
            if (!data) $this.data('scrollspy', (data = new ScrollSpy(this, options)))
            if (typeof option == 'string') data[option]()
        })
    }

    $.fn.scrollspy.Constructor = ScrollSpy

    $.fn.scrollspy.defaults = {
        offset: 10
    }


    /* SCROLLSPY DATA-API
     * ================== */

    $(function () {
        $('[data-spy="scroll"]').each(function () {
            var $spy = $(this)
            $spy.scrollspy($spy.data())
        })
    })

}(window.jQuery);