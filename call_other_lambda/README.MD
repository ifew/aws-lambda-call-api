## deploy
```
dotnet lambda deploy-function test_call_other_lambda –-function-role MyRole
```

## Test
```
docker run --rm -v "$PWD":/var/task -e AWS_LAMBDA_FUNCTION_MEMORY_SIZE=128 -e AWS_LAMBDA_FUNCTION_TIMEOUT=30  lambci/lambda:dotnetcore2.1  call_other_lambda::call_other_lambda.Function::FunctionHandler '""'
```