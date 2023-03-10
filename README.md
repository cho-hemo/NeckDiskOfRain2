# NeckDiskOfRain2
리스크 오브 레인 2 모작

# tag
v0.0.1

# =====Commit Convetion(커밋 규칙)=====
# [이름] 활동 대상 (전부 소문자)
## EX)
```
"[cho] setup project"
```


# =====Naming Convetion(명명 규칙)=====

## PascalCase : 단어의 시작은 대문자 이어지는 단어마다 대문자
## camelCase : 단어의 시작은 소문자 이어지는 단어마다 대문자

# Class, Method, Property, Enum, delegate / PascalCase(파스칼)
##  Ex) 
```
public class ExamClass
```

# InterFace / PascalCase + prefix "I"(파스칼 + 접두사 "I")
##  Ex) 
```
public Interface IExamInterface
```

# public Field / PascalCase(파스칼)
## Ex) 
```
public bool IsValid;
```

# private Field / CamelCase + prefix "_" (카멜 + 접두사 "_")
## Ex) 
```
private bool _isValid;
```

# Static Field / prefix "s_" + CamelCase (접두사 "s_" + 카멜)
## Ex) 
```
public static int s_num;
```

# =====Const Convention(Enum, 상수 등)=====
## Ex)
```
public enum ExamEnum
{
    EXAM1,EXAM2...
}
const string EXAM_STRING = "..."
```

# =====Method Annotation(함수 주석)=====
## Ex) 자동완성 주석 활용
```
///<summary>
///내용
///</summary>
///<param name ="변수명">내용</param>
```

# =====Bracket Convention(괄호 컨벤션)=====
# BSD
## Ex)
```
if(...)
{
    ...
}
```
