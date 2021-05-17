$(document).ready(function () {
    const filterBrand = $('.js-filter-brand');
    const filterCategory = $('.js-filter-category');
    const filterPrice = $('.js-filter-price');
    const btnLoadMore = $('.js-order-loading-container .btn-loadmore');
    const orderLoadingContainer = $('#order-loading');
    const urlApi = $('#orderLoadingUrl').val() || '';
    const currentLanguage = $('#currentLanguage').val() || 'en';
    let isLoading = false;
    let pageNumber = 1;

    const filterVal = {
        language: currentLanguage
    }

    // fetch data order loading
    function fetchOrderLoading(isInit = true) {
        const orderLoadingSection = $('.js-order-loading-container .js-order-loading');
        if (isInit) {
            pageNumber = 1;
        }
        const url = genUrl(urlApi, { pageNumber });
        $.get(url, function (res) {
            if (res) {
                orderLoadingContainer.data('more', res.HasMore);
                $('.order-list-loading').attr('hidden', true);

                if (isInit) {
                    orderLoadingSection.empty();
                }

                orderLoadingSection.append(res.Html || '');
                isLoading = false;
                $(document).ready(function () {
                    $('.tree-table').simpleTreeTable({
                        opened: 0
                    });
                });
            }
        });
    }

    (function infinityScroll() {
        $(window).scroll(function () {
            if (orderLoadingContainer.data('more')) {
                if (window.pageYOffset + window.innerHeight + 100 >= orderLoadingContainer.offset().top + orderLoadingContainer.height() && !isLoading) {
                    isLoading = true;
                    pageNumber++;
                    $('.order-list-loading').attr('hidden', false);
                    fetchOrderLoading(false);
                }
            }
        })
    })()
});

function genUrl(url, body, removeUnderfine = false) {
    const params = [];
    for (const [key, value] of Object.entries(body)) {
        if (removeUnderfine && !!value || !removeUnderfine) {
            params.push(`${key}=${value}`);
        }
    }
    return params.length ? `${url}?${params.join('&')}` : url;
}
