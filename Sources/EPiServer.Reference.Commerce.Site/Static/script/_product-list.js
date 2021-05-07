$(document).ready(function () {
    const filterBrand = $('.js-filter-brand');
    const filterCategory = $('.js-filter-category');
    const filterPrice = $('.js-filter-price');
    const btnLoadMore = $('.js-product-list-container .btn-loadmore');
    const productListContainer = $('#product-list');
    const urlApi = $('#productListingUrl').val() || '';
    let isLoading = false;
    let pageNumber = 1;
    let isSortDes = false;
    const filterVal = {
        brand: filterBrand.find(':selected').val() || filterBrand.val(),
        category: filterCategory.find(':selected').val() || filterCategory.val(),
        price: filterPrice.find(':selected').val()|| filterPrice.val()||0,
    }

    // init change value select
    selectValueChange(filterBrand, 'brand', filterVal, fetchProductList);
    selectValueChange(filterCategory, 'category', filterVal, fetchProductList);
    selectValueChange(filterPrice, 'price', filterVal, fetchProductList);

    $('#product-list .btn-sort').click(function () {
        isSortDes = !isSortDes;
        fetchProductList(true);
    })

    // fetch data product list
    function fetchProductList(isInit = true) {
        const productListSection = $('.js-product-list-container .js-product-list');
        if (isInit) {
            pageNumber = 1;
        }
        const url = genUrl(urlApi, { ...filterVal, isSortDes, pageNumber });
        $.get(url, function (res) {
            if (res) {
                productListContainer.data('more', res.HasMore);
                $('.product-list-loading').attr('hidden', true);

                if (isInit) {
                    productListSection.empty();
                }

                productListSection.append(res.Html || '');
                isLoading = false;
            }
        });
    }

    (function infinityScroll() {
        $(window).scroll(function () {
            if (productListContainer.data('more')) {
                if (window.pageYOffset + window.innerHeight + 250 >= productListContainer.offset().top + productListContainer.height() && !isLoading) {
                    isLoading = true;
                    pageNumber++;
                    $('.product-list-loading').attr('hidden', false);
                    fetchProductList(false);
                }
            }
        })
    })()
});

// function change value select
function selectValueChange(ele, param, filterVal, callback) {
    ele.change(() => {
        filterVal[param] = ele.val();
        changeUrl(filterVal);
        callback();
    });
}

// init url get
function genUrl(url, body, removeUnderfine = false) {
    const params = [];
    for (const [key, value] of Object.entries(body)) {
        if (removeUnderfine && !!value || !removeUnderfine) {
            params.push(`${key}=${value}`);
        }
    }
    return params.length ? `${url}?${params.join('&')}` : url;
}

function changeUrl(filterVal) {
    console.log("change url");
    const url = window.location.href.split('?').shift();
    const newUrl = genUrl(url, filterVal, true);
    window.history.pushState({ path: newUrl }, '', newUrl);
}