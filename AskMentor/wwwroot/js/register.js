var role = "";

$(document).ready(function () {
    $(".items-signup").click(function (event) {
        $(".items-signup").removeClass("active");
        $(this).addClass("active");
        $(".circle-main, .inner-circle").removeClass("active");
        $(this).find(".circle-main, .inner-circle").addClass("active");
        $("[data-qa='btn-apply']").removeAttr("disabled");
        $("[data-qa='btn-apply']").addClass("active");
        var dataCV = $(this).attr("data-cv");
        var dataButton = $(this).attr("data-button");
        $("[data-qa='btn-apply']").text(dataButton);
        console.log(dataCV);
        if (dataCV === 'client') {
            role = "Leaner"
        } else {
            role = "Mentor";

            $(".d-none").removeClass("d-none")

        }
        event.stopPropagation();
    });
    $("[data-qa='btn-apply']").click(function (event) {
        $("#createAccount").hide();
        $(".page-res").show();
        $(".right-header").show().css("display", "flex");

    })
});

function CreateAcc() {

    // Lấy thông tin từ form
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const address = document.getElementById('address').value;
    const university = document.getElementById('university').value;
    const major = document.getElementById('major').value;
    const avt = document.getElementById('avt').files[0];

    let formData = new FormData();

    formData.append("Email", email)
    formData.append("Password", password)
    formData.append("Name", `${firstName} ${lastName}`)
    formData.append("Role", role)
    formData.append("University", university)
    formData.append("Address", address)
    formData.append("Major", major)
    formData.append("Avt", avt)

    // Gửi yêu cầu đăng ký đến API
    fetch('/api/Authencation/register', {
        method: 'POST',
        headers: {

        },
        body: formData
    })
        .then(response => response.json())
        .then(data => {

            if (!data.status) {
                alert(data.message);
                return
            }

            // Xử lý dữ liệu trả về từ API ở đây
            console.log(data);
            localStorage.setItem("email", email)
            if (role === "Mentor") {
                window.location.href = "/Auth/EditProfile"
            } else {
                window.location.href = "/"
            }
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
}
