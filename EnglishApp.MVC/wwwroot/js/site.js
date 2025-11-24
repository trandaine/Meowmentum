// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//==== Hàm gắn title, bodyContent cho tất cả modal ===//
/**
 * Hàm mở modal với title và bodyHTML được truyền vào
 * @param {any} title - tiêu đề modal
 * @param {any} bodyHTML - nội dung HTML của modal
 */
function openModal(title, bodyHTML) {
    let divModalHeader = document.getElementById("divModalHeader");
    if (divModalHeader) {
        console.log("Setting modal title:", title);
        divModalHeader.innerText = title;
    }

    let divModalBody = document.getElementById("divModalBody");
    if (divModalBody) {
        console.log("Setting modal body HTML");
        divModalBody.innerHTML = bodyHTML;
    }
    let buttonModal = document.getElementById("buttonModal");
    //console.log(buttonModal)

    // if(buttonModal != null && buttonModal != undefined) {
    //     buttonModal.click();
    // }
    if (buttonModal) {
        console.log("Button is available!");
        buttonModal.click();
    }
}


//====  ===//
/**
 * Hàm đóng modal
 */
function closeModal() {
    let buttonModal = document.getElementById("buttonModal");
    if (buttonModal) {
        console.log("Closing modal...");
        buttonModal.click();
    }
}


/**
 * Hàm reload lại component danh sách lesson sau khi tạo mới hoặc xóa
 * @param {int} courseId - truyền vào id của khoá học hiện tại
 */
function reloadLessonList(courseId) {
    fetch(`/Lesson/LessonList?courseId=${courseId}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('lesson-list').innerHTML = html;
        });
}

/**
 * Hàm hiển thị loading screen trong thẻ HTML được truyền vào
 * @returns
 */
function displayLoadingScreen() {
    const container = document.getElementById(targetElementId);
    if (!container) {
        console.warn(`Element with ID '${targetElementId}' not found.`);
        return;
    }

    container.innerHTML = '<div class="spinner">Loading...</div>';
}

/**
 * Hàm reload lại 1 component trên trang truyền vào một URL và id của thẻ HTML cần load lại nội dung
 * @param {string} url - đường dẫn đến action trong controller trả về component view vd: '/{Controller}/{Action}'
 * @param {string} targetElementId - id của thẻ html cần load lại nội dung vd: 'divCoursesList'
 */
function reloadComponentView(url, targetElementId) {
    fetch(url)
        .then(response => response.text())
        .then(html => {
            const container = document.getElementById(targetElementId);
            if (container) {
                container.innerHTML = html;
            } else {
                console.warn(`Element with ID '${targetElementId}' not found.`);
            }
        })
        .catch(error => {
            console.error(`Failed to load content from ${url}:`, error);
        });
}


// === Hàm refresh lại trang ===//
function refreshPage() {
    location.reload();
}



/**
 * Hàm ajax gọi lệnh mở modal Delete cho controller Lesson
 * @param {any} idLesson - id của lesson cần xóa
 */
function openDeleteModalLesson(idLesson) {
    $.ajax({
        url: '/Lessons/Delete', // Đường dẫn đến API hoặc file server
        type: "GET", // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json", // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: { // Object dữ liệu gửi lên server ở dạng [key: value]
            id: idLesson,
            // parameter12: "value2"
        },
        // contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Sending Open Modal Delete Selected Lesson request...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            // console.log("Kết quả:", response);
            // openDeleteModal("Course Delete", response);
            openModal("Lesson Delete", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Sending Open Modal Delete Selected Lesson AJAX request completed!");
        },
        timeout: 10000 // Giới hạn thời gian (ms)
    });
}



//==== ===//
/**
 * Hàm Ajax gọi lệnh xóa lesson cho controller Lesson
 * @param {int} idLesson - id của lesson cần xóa
 */
function deleteLesson(idLesson) {
    let inputToken = document.querySelector("input[name='__RequestVerificationToken']");
    let token = inputToken ? inputToken.value : "";
    $.ajax({
        url: "/Lessons/Delete",    // Đường dẫn đến API hoặc file server
        type: "POST",                 // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json",            // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: {                      // Object dữ liệu gửi lên server ở dạng [key: value]
            id: idLesson,
            __RequestVerificationToken: token,
        },
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Sending Delete Selected Lesson request...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            console.log("Kết quả:", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Sending Delete Selected Lesson AJAX request complete");
            refreshPage();
        },
        timeout: 10000               // Giới hạn thời gian (ms)
    });
}


//====  ===//
/**
 * Hàm Ajax gọi lệnh mở modal Edit cho controller Lesson
 * @param {int} idLesson - id của lesson cần chỉnh sửa
 */
function openModalEditLesson(idLesson) {
    $.ajax({
        url: "/Lessons/Edit", // Đường dẫn đến API hoặc file server
        type: "GET", // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json", // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: { // Object dữ liệu gửi lên server ở dạng [key: value]
            id: idLesson,
            // parameter12: "value2"
        },
        // contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Sending Open Edit Lesson Modal request...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            // console.log("Kết quả:", response);
            // openEditModal("Edit course", response);
            openModal("Edit Lesson", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Open Edit Lesson Modal AJAX request complete");
        },
        timeout: 10000 // Giới hạn thời gian (ms)
    });
}



//==== ===//
/**
 * Hàm Ajax gọi lệnh lưu modal edit cho controller Lesson dưới dạng form submit
 */
function saveModalEditLessonForm() {
    var $form = $('#divModal form'); // Select the form inside the modal
    var formData = $form.serialize(); // Serialize all form fields
    // serialize() grabs all input values, including hidden fields and the anti-forgery token.

    $.ajax({
        url: "/Lessons/Edit",
        type: "POST",
        data: formData,
        beforeSend: function () {
            console.log("Sending edit lesson request...");
        },
        success: function (response) {
            console.log("Result:", response);
            closeModal();
            reloadLessonList(dtoLesson.courseId);

            //refreshPage();
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            console.log("Details:", xhr.responseText);
        },
        complete: function () {
            console.log("Edit Lesson AJAX request complete");
        },
        timeout: 10000
    });
}


function getEditLessonData() {
    let formCreateLesson = document.getElementById("formEditLesson");
    let inputId = formCreateLesson.querySelector('input[name="Id"]');
    let inputName = formCreateLesson.querySelector('input[name="Name"]');
    let inputContent = formCreateLesson.querySelector('input[name="Content"]');
    let selectedContentType = formCreateLesson.querySelector('select[name="ContentType"]');
    let inputVideo = formCreateLesson.querySelector('input[name="Video"]');
    let inputAudio = formCreateLesson.querySelector('input[name="Audio"]');
    let inputCourseId = formCreateLesson.querySelector('input[name="CourseId"]');
    let dtoEditLesson = {
        id: Number(inputId?.value),
        name: inputName?.value,
        content: inputContent?.value,
        contentType: Number(selectedContentType?.value),
        video: inputVideo?.value,
        audio: inputAudio?.value,
        courseId: Number(inputCourseId?.value)
    };
    return dtoEditLesson;
}


/**
 * Hàm Ajax gọi lệnh lưu modal edit cho controller Lesson dưới dạng object submit
 */
function saveModalEditLesson() {
    const token = $('input[name="__RequestVerificationToken"]').val();

    let dtoLesson = getEditLessonData();

    $.ajax({
        url: "/Lessons/Edit",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dtoLesson),
        headers: { "RequestVerificationToken": token },
        beforeSend: function () {
            console.log("Sending edit lesson request...");
        },
        success: function (response) {
            console.log("Result:", response);
            if (response && response.isOkay) {
                closeModal();
                reloadLessonList(dtoLesson.courseId);
            }
            //refreshPage();
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            console.log("Details:", xhr.responseText);
        },
        complete: function () {
            console.log("Edit Lesson AJAX request complete");
        },
        timeout: 10000
    });
}

//====  ===//
/**
 * Hàm Ajax gọi lệnh mở modal create tạo lesson mới cho LessonController kèm theo courseId của khóa học đang xem hiện tại
 * @param {int} courseId - id của khóa học hiện tại
 */
function openModalCreateLesson(courseId) {
    $.ajax({
        url: "/Lessons/Create",
        type: "GET", // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json", // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: { // Object dữ liệu gửi lên server ở dạng [key: value]
            courseId: courseId
        },
        // contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Sending open modal CREATE lesson request...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            // console.log("Kết quả:", response);
            // openDeleteModal("Course Delete", response);
            openModal("Create a new Lesson", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Open Modal Create Lesson AJAX request complete");
        },
        timeout: 10000 // Giới hạn thời gian (ms)
    });
}







/**
 * Hàm Ajax gọi lệnh lưu modal create/edit cho controller Courses dưới dạng form submit
 * @param {any} actionType
 */
function submitCourseForm(actionType) {
    var $form = $('#divModal form');
    var formData = new FormData($form[0]); // includes files + antiforgery token
    //var formData = $form.serialize(); // Serialize all form fields


    $.ajax({
        url: actionType === 'Create' ? '/Courses/Create' : '/Courses/Edit',
        type: "POST",
        data: formData,
        processData: false, // prevent jQuery from processing data
        contentType: false, // prevent jQuery from setting content type
        beforeSend: function () {
            console.log("Sending " + actionType + " request...");
        },
        success: function (response) {
            if (response.success) {
                closeModal();
                refreshPage();
            } else {
                // Replace modal body with returned partial (with errors)
                $("#divModalBody").html(response);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error on Create/Edit Courses Action:", error);
            console.log("Details:", xhr.responseText);
        },
        complete: function () {
            console.log(actionType + " AJAX request complete");
        },
        timeout: 10000
    });
}


/**
* Hàm Ajax gọi lệnh mở modal Edit cho controller Courses
*/
function openModalEditCourse(idCourse) {
    $.ajax({
        //url: '@Url.Action("Edit", "Courses")', // Đường dẫn đến API hoặc file server
        url: '/Courses/Edit', // Đường dẫn đến API hoặc file server
        type: "GET", // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json", // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: { // Object dữ liệu gửi lên server ở dạng [key: value]
            id: idCourse,
            // parameter12: "value2"
        },
        // contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Đang gửi yêu cầu...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            // console.log("Kết quả:", response);
            // openEditModal("Edit course", response);
            openModal("Edit course", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Hoàn tất AJAX request");
        },
        timeout: 10000 // Giới hạn thời gian (ms)
    });
}

//==== ===//
/**
 * Hàm Ajax gọi lệnh triển khai modal create tạo lesson mới cho LessonController kem theo courseId của khoá học hiện đang xem dưới dạng form submit
 */
function createLessonFormSubmit() {
    //var form = $('#divModal form'); // Select the form inside the modal
    //var formData = $form.serialize(); // Serialize all form fields


    var form = document.getElementById("formCreateLesson"); // Select the form inside the modal
    var formData = new FormData(form); // Serialize all form fields
    console.log("formData:", formData);


    let lessonModel = {
        id: formData.get('Id'),
        name: formData.get('Name'),
        content: formData.get('Content'),
        contentType: formData.get('ContentType'),
        video: formData.get('Video'),
        audio: formData.get('Audio'),
        courseId: formData.get('CourseId'),
        //__RequestVerificationToken: formData.__RequestVerificationToken,
    };
    console.log("lessonModel:", lessonModel);



    // serialize() grabs all input values, including hidden fields and the anti-forgery token.
    $.ajax({
        url: "/Lessons/Create",
        type: "POST",
        data: {
            lessonModel,
            __RequestVerificationToken: formData.get('__RequestVerificationToken'),

        },

        beforeSend: function () {
            console.log("Sending create LESSON request...");
        },
        success: function (response) {
            console.log("Result:", response);
            if (response.isOkay) {
                closeModal();
                refreshPage();
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            console.log("Details:", xhr.responseText);
            //var $f = $('#divModal form');
            //console.log($f.length, $f.serialize());
            //console.log("token:", !!$f.find('input[name=__RequestVerificationToken]').val());
            //console.log("courseId:", $f.find('input[name=CourseId]').val());
        },
        complete: function () {
            console.log("Create NEW LESSON AJAX request complete");
        },
        timeout: 10000
    });
}

/**
 * Hàm Ajax reload lại danh sách lesson sau khi tạo mới hoặc xóa
 * @param {int} courseId - truyền vào id của khoá học hiện tại
 */
function reloadLessonList(courseId) {
    $.ajax({
        url: "/Lessons/LessonComponentList",    // Đường dẫn đến API hoặc file server
        type: "GET",                 // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json",            Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: {                      //Object dữ liệu gửi lên server ở dạng [key: value]
            courseId: courseId,
            // parameter12: "value2"
        },
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Đang gửi yêu cầu...");
        },
        success: function (response) {
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị
            let lessonListContainer = document.getElementById("listLessonContainer");
            if (lessonListContainer) {
                lessonListContainer.innerHTML = response;
            }

            console.log("Kết quả:", response);
        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Hoàn tất AJAX request");
        },
        timeout: 10000               // Giới hạn thời gian (ms)
    });
}

/**
 * Hàm lấy dữ liệu từ form tạo lesson mới tạo thành object DTO
 * @returns
 */
function getCreateLessonData() {
    let formCreateLesson = document.getElementById("formCreateLesson");
    let inputId = formCreateLesson.querySelector('input[name="Id"]');
    let inputName = formCreateLesson.querySelector('input[name="Name"]');
    let inputContent = formCreateLesson.querySelector('input[name="Content"]');
    let selectedContentType = formCreateLesson.querySelector('select[name="ContentType"]');
    let inputVideo = formCreateLesson.querySelector('input[name="Video"]');
    let inputAudio = formCreateLesson.querySelector('input[name="Audio"]');
    let inputCourseId = formCreateLesson.querySelector('input[name="CourseId"]');
    let dtoCreateLesson = {
        id: Number(inputId?.value),
        name: inputName?.value,
        content: inputContent?.value,
        contentType: Number(selectedContentType?.value),
        video: inputVideo?.value,
        audio: inputAudio?.value,
        courseId: Number(inputCourseId?.value)
    };
    return dtoCreateLesson;
}


//==== ===//
// Sends JSON object and passes anti-forgery token via header
/**
 * Hàm Ajax gọi lệnh triển khai modal create tạo lesson mới cho LessonController kèm theo courseId của khóa học đang xem hiện tại gửi kèm theo token dưới dạng object
 * @returns {void}
 */
function createLesson() {
    const token = $('input[name="__RequestVerificationToken"]').val();

    let dtoLesson = getCreateLessonData();
    //let token = document.querySelector('input[name="__RequestVerificationToken"]');


    console.log("dtoLesson:", dtoLesson);
    console.log("Anti-forgery token:", token);

    // Validate Content Type if user choose '-- Select Content Type ---'
    if (!dtoLesson.contentType || Number.isNaN(dtoLesson.contentType)) {
        console.warn("Please select a valid Content Type.");
        return;
    }

    $.ajax({
        url: "/Lessons/Create",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dtoLesson),
        headers: { "RequestVerificationToken": token },
        beforeSend: function () {
            console.log("Sending create LESSON (JSON) request...");
        },
        success: function (response) {
            console.log("Result:", response);
            if (response && response.isOkay) {
                closeModal();
                reloadLessonList(dtoLesson.courseId);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            console.log("Details:", xhr.responseText);
        },
        complete: function () {
            console.log("Create NEW LESSON (JSON) AJAX request complete");
        },
        timeout: 10000
    });







}



/**
 * Hàm tăng số lượng item trong giỏ hàng
 * @param {any} itemId
 */
function increaseItem(itemId) {
    $.ajax({
        //url: '@Url.Action("IncreaseItemQuantity", "Cart")',
        url: '/Cart/IncreaseItemQuantity',
        type: "POST",
        data: {
            cartItemId: itemId
        },
        success: function (response) {
            if (window.location.pathname.toLowerCase().startsWith('/shoppingcarts/details')) {
                refreshPage();
            } else {
                reloadComponentView('/ShoppingCarts/GetCartQuantityComponentView', 'divCartCountContainer');
                reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'cartItemsDropdown');
                // reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'divCartItemsDetails');
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi:", error);
        }
    });
}

/**
 * Hàm giảm số lượng item trong giỏ hàng
 * @param {any} itemId
 */
function decreaseItem(itemId) {
    $.ajax({
        //url: '@Url.Action("DecreaseItemQuantity", "Cart")',
        url: '/Cart/DecreaseItemQuantity',
        type: "POST",
        data: {
            cartItemId: itemId
        },
        success: function (response) {
            if (window.location.pathname.toLowerCase().startsWith('/shoppingcarts/details')) {
                refreshPage();
            } else {
                reloadComponentView('/ShoppingCarts/GetCartQuantityComponentView', 'divCartCountContainer');
                reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'cartItemsDropdown');
                // reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'divCartItemsDetails');
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi:", error);
        }
    });
}




/**
* Hàm thực hiện thêm thiết bị vào giỏ hàng
*/
function addToCart(idCourse, quantity = 1) {
    $.ajax({
        url: '/Cart/AddToCart',
        type: 'POST',
        data: {

            productId: idCourse,
            quantity: quantity
        },
        beforeSend: function (xhr) {  // 🟡 Hàm gọi trước khi gửi request
            console.log('Đang gửi request...');
        },

        success: function (response) { // 🟢 Khi request thành công
            console.log('Thành công:', response);
            reloadComponentView('/ShoppingCarts/GetCartQuantityComponentView', 'divCartCountContainer')
            reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'cartItemsDropdown')
            // let spanShoppingCart = document.getElementById("spanShoppingCart");
            // if(spanShoppingCart) {
            //     spanShoppingCart.innerText = response;
            // }
        },

        error: function (xhr, status, error) { // 🔴 Khi có lỗi xảy ra
            // console.error('Lỗi:', status, error);
            if (xhr.status === 401) {
                // Redirect user to login page
                window.location = "/Authentication/Login";
            } else {
                alert("Error: " + xhr.status);
            }
        },

        complete: function (xhr, status) { // ⚪ Dù thành công hay lỗi, luôn chạy sau cùng
            console.log('Hoàn tất request.');
        }
    });
}



/**
 * Hàm xóa item trong giỏ hàng 
 * @param {any} itemId
 */
function removeItemInShoppingCart(itemId) {
    // Implement item removal logic here
    $.ajax({
        //url: '@Url.Action("Delete", "Cart")', // Đường dẫn đến API hoặc file server
        url: '/Cart/Delete', // Đường dẫn đến API hoặc file server
        type: "POST", // Hoặc "POST", "PUT", "DELETE"
        // dataType: "json", // Kiểu dữ liệu server trả về: json, html, text, xml, script
        data: { // Object dữ liệu gửi lên server ở dạng [key: value]
            // parameter12: "value2"
            id: itemId
        },
        // contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        beforeSend: function () {
            // Hàm gọi trước khi gửi request: thường hiện trạng thái Loading
            console.log("Đang gửi yêu cầu...");
        },

        success: function (response) {
            // Check if current page is ShoppingCarts/Details
            if (window.location.pathname.toLowerCase().startsWith('/shoppingcarts/details')) {
                refreshPage();
            } else {
                reloadComponentView('/ShoppingCarts/GetCartQuantityComponentView', 'divCartCountContainer');
                reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'cartItemsDropdown');
                // reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'divCartItemsDetails');
            }
            // Hàm gọi khi request thành công: Thường cập nhật lại dữ liệu hiển thị\
            // refreshPage()
            // console.log("Kết quả:", response);
            // reloadComponentView('/ShoppingCarts/GetCartQuantityComponentView', 'divCartCountContainer')
            // reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'cartItemsDropdown')
            // reloadComponentView('/ShoppingCarts/GetShoppingCartItemListComponentView', 'divCartItemsDetails')
            // openDeleteModal("Course Delete", response);

        },
        error: function (xhr, status, error) {
            // Khi có lỗi xảy ra: thường hiển thị lỗi trên modal hoặc thông báo lỗi
            console.error("Lỗi:", error);
            console.log("Chi tiết:", xhr.responseText);
        },
        complete: function () {
            // Dù thành công hay thất bại đều chạy: Thường dùng để đóng trạng thái Loading, đóng Modal đang mở
            console.log("Hoàn tất AJAX request");
        },
        timeout: 10000 // Giới hạn thời gian (ms)
    });
}








//$(document).ready(function () {
//    // Intercept the form submit inside the modal
//    $(document).on("submit", "#divModal form", function (e) {
//        e.preventDefault(); // stop browser navigation
//        submitCourseForm("Create"); // or "Edit" depending on context
//    });
//});
