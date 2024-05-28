

document.getElementById('loginForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Ngăn chặn form gửi đi mặc định

    // Lấy thông tin từ form
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Gửi yêu cầu đăng nhập đến API
    fetch('/api/Authencation/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Email: email,
            Password: password
        })
    })
        .then(response => response.json())
        .then(data => {
            // Xác minh xem phản hồi có thành công không
            if (data.status === true) {
                // Lưu token vào local storage hoặc session storage
                localStorage.setItem('token', data.token);

                // Hiển thị thông báo thành công hoặc chuyển hướng người dùng đến trang chính
                alert(data.message);
                window.location.href = '/home'; // Chuyển hướng đến trang chính sau khi đăng nhập thành công
            } else {
                // Hiển thị thông báo lỗi
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
});

function HandleEmail() {
    let email = document.getElementById("email2").value;
    let pass = document.getElementById("pass").value;

    fetch('/api/Authencation/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Email: email,
            Password: pass
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.status === true) {

                localStorage.setItem('token', data.token);

                if (data.role.includes("Admin")) {
                    window.location.href = '/Admin/Index';
                } else {
                    window.location.href = '/';
                }

            } else {
                // Hiển thị thông báo lỗi
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
}
