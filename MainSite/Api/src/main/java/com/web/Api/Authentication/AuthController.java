package com.web.Api.Authentication;

import jakarta.annotation.security.PermitAll;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(path = "/api/auth")
@PermitAll
public class AuthController {

    @PostMapping("/login")
    public ResponseEntity<String> LoginRequest(@RequestBody LoginRequest request){

        return ResponseEntity.ok("testas for noew");
    }
}
