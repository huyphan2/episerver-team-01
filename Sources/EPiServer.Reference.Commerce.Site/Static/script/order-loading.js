var order = {
    init: function () {
        order.loadingTable(order.loadingUrl);
        order.searching();
    },
    pageNumber: 1,
    term:'',
    loadingUrl: $('#orderLoadingUrl').val() || '',
    isLoading: false,
    loadingContainer: $('#order-loading'),
    loadingSection: $('.js-order-loading-container .js-order-loading'),
    fetchLoading: function (isInit = true, urlApi = '', params) {
        if (isInit) {
            order.pageNumber = 1;
        }
        const url = renderUrl(urlApi, { ...params, pageNumber: order.pageNumber });
        $.get(url, function (res) {
            if (res) {
                order.loadingContainer.data('more', res.HasMore);
                $('.order-list-loading').attr('hidden', true);

                if (isInit) {
                    order.loadingSection.empty();
                }

                order.loadingSection.append(res.Html || '');
                order.isLoading = false;
                $(document).ready(function () {
                    $('.tree-table').simpleTreeTable();
                });
            }
        });
    },
    loadingTable: function () {

        (function infinityScroll() {
            $(window).scroll(function () {
                if (order.loadingContainer.data('more')) {
                    if (window.pageYOffset + window.innerHeight >= order.loadingContainer.offset().top + order.loadingContainer.height() && !order.isLoading) {
                        order.isLoading = true;
                        order.pageNumber++;
                        $('.order-list-loading').attr('hidden', false);
                        order.fetchLoading(false, $('#orderLoadingUrl').val());
                    }
                }
            })
        })()
    },
    searching: function () {
        $('.quick-search').on('keyup', function () {
            let $this = $(this);
            let { urlApi } = $this.data();
            order.fetchLoading(true, urlApi, { term: $this.val() });

        });
    }

}

$(document).ready(function () {
    order.init();
});

function renderUrl(url, body, removeUnderfine = false) {
    const params = [];
    for (const [key, value] of Object.entries(body)) {
        if (removeUnderfine && !!value || !removeUnderfine) {
            params.push(`${key}=${value}`);
        }
    }
    return params.length ? `${url}?${params.join('&')}` : url;
}
